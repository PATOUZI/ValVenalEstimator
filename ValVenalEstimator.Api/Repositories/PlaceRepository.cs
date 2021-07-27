using AutoMapper;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ValVenalEstimator.Api.Contracts;
using ValVenalEstimator.Api.Models;
using ValVenalEstimator.Api.ViewModels;
using ValVenalEstimator.Api.Data;

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
        public async Task<Place> GetPlaceAsync(long id)
        {            
            //var place = await _valVenalEstDbContext.Places.FindAsync(id);
            //if (place == null)
            //{
                //return null;
            //}
            //return place;

            //Tout ceci est fait pour pouvoir récupérer la zone
            Place place = null;
            var listPlaces = await _valVenalEstDbContext.Places.Include(p => p.Zone).ToListAsync(); 
            foreach (var p in listPlaces)
            {
                if (p.Id == id)
                {
                    place = new Place();
                    place = p;
                    break;
                }
            }          
            if (place == null)
            {
               throw new Exception("La quartier avec l'id "+id+" n'existe pas !!!");
            }
            //var resource = _mapper.Map<Zone, ZoneViewDTO>(place.Zone);
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
        public async void SaveChangeAsync()
        {
            await _valVenalEstDbContext.SaveChangesAsync();
        }
        public bool PlaceExists(long id) =>
            _valVenalEstDbContext.Places.Any(p => p.Id == id);
        
        public void Remove(Place place)
        {
            _valVenalEstDbContext.Places.Remove(place);   
        }
    }
}                             