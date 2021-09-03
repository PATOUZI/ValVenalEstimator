using CsvHelper;
using AutoMapper;
using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValVenalEstimator.Api.Contracts;
using ValVenalEstimator.Api.Data;
using ValVenalEstimator.Api.Models;
using ValVenalEstimator.Api.ViewModels;

namespace ValVenalEstimator.Api.Repositories
{
    public class PlaceRepository : IPlaceRepository
    {
        readonly ValVenalEstimatorDbContext _valVenalEstDbContext;
        readonly IZoneRepository _izoneRepository;
        private readonly IMapper _mapper;
        public PlaceRepository(ValVenalEstimatorDbContext context, IZoneRepository izoneRepository, IMapper mapper)
        {
            _valVenalEstDbContext = context;
            _izoneRepository = izoneRepository;
            _mapper = mapper;
        }

        /*Cette méthode permet d'enrégistrer une place à partir 
        du nom du quartier et de l'Id de la zone(PlaceCsvDTO)*/
        public async Task<Place> AddPlaceAsync(PlaceDTO placeDTO)
        {
            Place p = placeDTO.ToPlace();
            var zone = await _izoneRepository.GetZoneAsync(p.ZoneId);
            if (zone != null)
            {
                p.Zone = zone;
                await _valVenalEstDbContext.AddAsync(p);
                SaveChanges();
                return p;
            }
            else
            {
                throw new Exception("La zone avec l'id " + placeDTO.ZoneId + " n'existe pas !!!");
            }
        }

        /*Cette méthode permet d'enrégistrer une place à partir 
        du nom du quartier, de la zone et de la prefecture(PlaceCsv2DTO)*/
        public async Task<Place> AddPlaceAsync2(PlaceCsv2DTO placeDTO)
        {
            var zone = _izoneRepository.GetZoneByZoneNameAndPrefectureNameAsync(placeDTO.ZoneName, placeDTO.PrefectureName);               
            if (zone != null)
            {
                Place p = placeDTO.ToPlace();
                p.ZoneId = zone.Id;
                p.Zone = zone.Result;
                await _valVenalEstDbContext.AddAsync(p);
                SaveChanges();
                return p;
            }
            else
            {
                throw new Exception("La zone correspondant à cette localité n'existe pas !!!");
            }
        }
        public async Task<Place> GetPlaceAsync(long id)
        {
            var place = await _valVenalEstDbContext.Places.Include(p => p.Zone).Where(p => p.Id == id).SingleOrDefaultAsync();
            if (place == null)
            {
                throw new Exception("La quartier avec l'id " + id + " n'existe pas !!!");
            }
            return place;
        }
        public async Task<PlaceViewDTO> GetPlaceViewDTOAsync(long id)
        {
            Place p = await GetPlaceAsync(id);
            var resource = _mapper.Map<Zone, ZoneViewDTO>(p.Zone);
            PlaceViewDTO placeViewDTO = new PlaceViewDTO();
            placeViewDTO.Id = p.Id;
            placeViewDTO.Name = p.Name;
            placeViewDTO.Zone = resource;
            return placeViewDTO;
        }
        public async Task<IEnumerable<Place>> GetAllPlacesAsync()
        {
            var listPlaces = await _valVenalEstDbContext.Places.OrderBy(p => p.Name).ToListAsync();
            return listPlaces;
        }
        public async Task<IEnumerable<Place>> GetPlacesByPrefectureIdAsync(long idPrefecture)
        {
            var listPlaces = await _valVenalEstDbContext.Places.Where(p => p.Zone.PrefectureId == idPrefecture)
                                                               .OrderBy(p => p.Name)
                                                               .ToListAsync();
            return listPlaces;
        }
        public async Task<IEnumerable<Place>> GetPlacesByZoneIdAsync(long idZone)
        {
            return await _valVenalEstDbContext.Places.Where(p => p.ZoneId == idZone)
                                                     .ToListAsync();
        }
        public async void LoadDataInDbWithCsvFile(string accessPath)
        {
            using (var reader = new StreamReader(accessPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<PlaceCsvDTO>();
                foreach (var p in records)
                {
                    PlaceDTO placeDTO = p.ToPlaceDTO();
                    await AddPlaceAsync(placeDTO);
                }
            }
        }

        /*Cette méthode permet d'enrégistrer des places grace à un fichier csv 
        contenant les noms des quartiers, des zones et des prefectures(PlaceCsv2DTO)*/
        public async void LoadData(string accessPath)
        {
            using (var reader = new StreamReader(accessPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<PlaceCsv2DTO>();
                foreach (var p in records)
                {
                    await AddPlaceAsync2(p);
                }
            }
        }
        public async Task<ActionResult<ResponseDTO>> GetPriceToPayAsync(long idPlace, int area, double valAchat, int nbrePge)
        {            
            double bornContra;
            double priceOfOnePge = 1500;
            double depositFee = 3000;
            ResponseDTO response = new ResponseDTO();
            var place = await GetPlaceViewDTOAsync(idPlace);
            double valVenalTerrain = place.Zone.PricePerMeterSquare * area;
            double valTaxable = Math.Max(valAchat, valVenalTerrain);
            double valEnregisitrement = Math.Ceiling((valTaxable * 0.015) + 10000);
            double droitDeTimbre = nbrePge * priceOfOnePge;
            double priceToPay = valEnregisitrement + droitDeTimbre;
            if (place.Name == "Terrains agricoles")
            {
                if (area <= 10000)
                {
                    bornContra = 70000;
                }
                else
                {
                    double additionalHectare = Math.Ceiling((double)(area - 10000) / 10000);
                    bornContra = 70000 + additionalHectare * 10000;
                }
            }
            else
            {
                if (area <= 600)
                {
                    bornContra = 60000 + depositFee;
                }
                else
                {
                    double additionalMeterSquare = Math.Ceiling((double)(area - 600) / 600);
                    bornContra = 60000 + additionalMeterSquare * 2000;
                }
            }
            response.CalculationBasis = valTaxable;
            response.PriceOfBornageContradictoire = bornContra;
            response.ValEnregistrement = valEnregisitrement;
            response.DroitDeTimbre = droitDeTimbre;
            response.ValVenal = valVenalTerrain;    
            response.PriceToPay = priceToPay;             
            response.PlaceName = place.Name;
            response.ZoneName = place.Zone.Name;
            response.ZoneType = place.Zone.Type;
            response.Area = area;
            return response;
        }
        public async void SaveChanges()
        {
            await _valVenalEstDbContext.SaveChangesAsync();
        }
        public bool PlaceExists(long id) => _valVenalEstDbContext.Places.Any(p => p.Id == id);
        public void Remove(Place place)
        {
            _valVenalEstDbContext.Places.Remove(place);
        }
    }
}