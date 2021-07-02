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
        public async Task<Place> AddAsyncPlace(Place place)
        {
            var existingZone = _izoneRepository.ZoneExists(place.ZoneId);
            if (existingZone == true)
            {
                var zone = _izoneRepository.GetAsyncZone(place.ZoneId);
                place.Zone = zone.Result;
                _valVenalEstDbContext.Add(place);
                await _valVenalEstDbContext.SaveChangesAsync();
                return place;
            } 
            else
            {
                return null;
            }        
        }
        public async Task<Place> GetAsyncPlace(long id)
        {
            var place = await _valVenalEstDbContext.Places.FindAsync(id); //Include(p => p.Zone)
            if (place == null)
            {
                return null;
            }
            return place;
        }
        public async Task<IEnumerable<Place>> GetAsyncAllPlaces()
        {
            return await _valVenalEstDbContext.Places.Include(p => p.Zone).ToListAsync();
        }                 
        public async Task<IActionResult> DeleteAsyncPlace(long id)
        {
            var place = await _valVenalEstDbContext.Places.FindAsync(id);
            if (place == null)
            {
                return null;     
            }     
            _valVenalEstDbContext.Places.Remove(place);                                      
            await _valVenalEstDbContext.SaveChangesAsync();
            return null;
        }
        public async Task<IEnumerable<Place>> GetAsyncPlacesByZoneId(long IdZone)
        {
             return await _valVenalEstDbContext.Places
                            .Where(o => o.ZoneId == IdZone)
                            .ToListAsync();
        }
        public async void LoadAsyncDataInDbWithCsvFile(string accessPath)
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
                    //AddPlace(place);
                }
            }
        }       
        public async void SaveAsyncChange()
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