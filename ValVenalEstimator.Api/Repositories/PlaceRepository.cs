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
        public async Task<Place> AddPlaceAsync(Place place)
        {
            var zone = await _izoneRepository.GetZoneAsync(place.ZoneId);
            if (zone != null)
            {
                place.Zone = zone;
                _valVenalEstDbContext.Add(place);
                await _valVenalEstDbContext.SaveChangesAsync(); //SaveChangeAsync()
                return place;
            } 
            else
            {
                throw new Exception("La prefecture avec l'id "+place.ZoneId+" n'existe pas !!!");
            }        
        }
        public async Task<Place> GetPlaceAsync(long id)
        {
            Place place = new Place();
            var listPlaces = await _valVenalEstDbContext.Places.Include(p => p.Zone).ToListAsync(); 
            foreach (var p in listPlaces)
            {
                if (p.Id == id)
                {
                    place = p;
                }
            }          
            if (place == null)
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
            return await _valVenalEstDbContext.Places.Include(p => p.Zone) //Pas de zone à enlever après test et verification
                                                     .Where(p => p.Zone.PrefectureId == idPrefecture)
                                                     .ToListAsync();            
        }              
        public async Task<IActionResult> DeletePlaceAsync(long id)
        {
            var place = await _valVenalEstDbContext.Places.FindAsync(id); //GetPlaceAsync()
            if (place == null)
            {
                return null;     
            }     
            _valVenalEstDbContext.Places.Remove(place);                                      
            await _valVenalEstDbContext.SaveChangesAsync(); //SaveChangeAsync();
            return null;
        }
        public async Task<IEnumerable<Place>> GetPlacesByZoneIdAsync(long idZone)
        {
             return await _valVenalEstDbContext.Places
                            .Where(p => p.ZoneId == idZone)
                            .ToListAsync();
        }
        public async void LoadDataInDbWithCsvFileAsync(string accessPath)
        {
            using (var reader = new StreamReader(accessPath))   
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<PlaceDTO>();
                foreach (var p in records)
                {
                    Place place = new Place();
                    place.Name = p.Name;
                    place.ZoneId = p.ZoneId;
                    _valVenalEstDbContext.Add<Place>(place);               
                    await _valVenalEstDbContext.SaveChangesAsync();
                    //AddPlaceAsync(place);
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