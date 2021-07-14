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
        
        public PlaceRepository(ValVenalEstimatorDbContext context, IZoneRepository izoneRepository)
        {  
            _valVenalEstDbContext = context; 
            _izoneRepository = izoneRepository;
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
            
            /*var place = await _valVenalEstDbContext.Places.FindAsync(id);
            if (place == null)
            {
                return null;
            }
            return place;*/

            Place place = new Place();
            var listPlaces = await _valVenalEstDbContext.Places.Include(p => p.Zone).ToListAsync(); 
            foreach (var p in listPlaces)
            {
                if (p.Id == id)
                {
                    place = p;
                }
            }          
            if (place.Id == 0)
            {
               throw new Exception("La quartier avec l'id "+id+" n'existe pas !!!");
            }
            return place;
        }
        public async Task<IEnumerable<Place>> GetAllPlacesAsync()
        {
            return await _valVenalEstDbContext.Places.Include(p => p.Zone).ToListAsync();
        }   
        public async Task<IEnumerable<Place>> GetPlacesByPrefectureIdAsync(long idPrefecture)
        {
            return await _valVenalEstDbContext.Places.Where(p => p.Zone.PrefectureId == idPrefecture)
                                                     .ToListAsync();            
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