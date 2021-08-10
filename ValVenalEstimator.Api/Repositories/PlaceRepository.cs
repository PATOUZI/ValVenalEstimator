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
        public async Task<Place> AddPlaceAsync(PlaceDTO placeDTO)
        {
            Place p = placeDTO.ToPlace();
            var zone = await _izoneRepository.GetZoneAsync(p.ZoneId);
            if (zone != null)
            {
                p.Zone = zone;
                await _valVenalEstDbContext.AddAsync(p);
                //await _valVenalEstDbContext.SaveChangesAsync(); 
                SaveChangeAsync();
                return p;
            } 
            else
            {
                throw new Exception("La prefecture avec l'id "+placeDTO.ZoneId+" n'existe pas !!!");
            }        
        }
        public async Task<Place> AddPlaceAsync2(PlaceCsv2DTO placeDTO)
        {
            var zone = _izoneRepository.GetZoneByZoneNameAndPrefectureNameAsync(placeDTO.ZoneName, placeDTO.PrefectureName);
            if (zone != null)
            {
                Place p = placeDTO.ToPlace();
                p.ZoneId = zone.Id;      
                p.Zone = zone.Result;
                await _valVenalEstDbContext.AddAsync(p);
                //await _valVenalEstDbContext.SaveChangesAsync(); 
                SaveChangeAsync();
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
               throw new Exception("La quartier avec l'id "+id+" n'existe pas !!!");
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
            var ListPlaces = await _valVenalEstDbContext.Places.ToListAsync();
            var res = ListPlaces.OrderBy(p => p.Name);
            return res;
        }   
        public async Task<IEnumerable<Place>> GetPlacesByPrefectureIdAsync(long idPrefecture)
        {
            var ListPlaces = await _valVenalEstDbContext.Places.Where(p => p.Zone.PrefectureId == idPrefecture)
                                                               .ToListAsync();      
            var res = ListPlaces.OrderBy(p => p.Name);
            return res;
        }              
        public async Task<IEnumerable<Place>> GetPlacesByZoneIdAsync(long idZone)
        {
             return await _valVenalEstDbContext.Places.Where(p => p.ZoneId == idZone)
                                                      .ToListAsync();
        }
        public async void LoadDataInDbWithCsvFileAsync(string accessPath)
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
        public async void LoadDataAsync(string accessPath)
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
        public async void SaveChangeAsync()
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