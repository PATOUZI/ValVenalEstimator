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
            var existingZone = _izoneRepository.ZoneExists(place.ZoneId);
            if (existingZone == true)
            {
                var zone = _izoneRepository.GetZoneAsync(place.ZoneId);
                place.Zone = zone.Result;
                _valVenalEstDbContext.Add(place);
                await _valVenalEstDbContext.SaveChangesAsync(); //SaveChangeAsync()
                return place;
            } 
            else
            {
                return null;
            }        
        }
        public async Task<Place> GetPlaceAsync(long id)
        {
            var place = await _valVenalEstDbContext.Places.FindAsync(id); //Include(p => p.Zone)
            if (place == null)
            {
                return null; //throw new Exception("La zone avec l'id "+id+" n'existe pas !!!");
            }
            return place;
        }
        public async Task<IEnumerable<Place>> GetAllPlacesAsync()
        {
            return await _valVenalEstDbContext.Places.Include(p => p.Zone).ToListAsync();
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
        public async Task<IEnumerable<Place>> GetPlacesByZoneIdAsync(long IdZone)
        {
             return await _valVenalEstDbContext.Places
                            .Where(o => o.ZoneId == IdZone)
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