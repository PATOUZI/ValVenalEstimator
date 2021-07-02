using System.Threading.Tasks;
using ValVenalEstimator.Api.Models;
using ValVenalEstimator.Api.ViewModels;
using ValVenalEstimator.Api.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using System.IO;
using System.Globalization;
using ValVenalEstimator.Api.Contracts;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<Place> AddPlace(Place place)
        {
            var existingZone = _izoneRepository.ZoneExists(place.ZoneId);
            if (existingZone == true)
            {
                var zone = _izoneRepository.GetZone(place.ZoneId);
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
        public async Task<Place> GetPlace(long id)
        {
            var place = await _valVenalEstDbContext.Places.FindAsync(id);
                                                           //Include(p => p.Zone)

            if (place == null)
            {
                return null;
            }
            return place;
        }
        public async Task<IEnumerable<Place>> GetAllPlaces()
        {
            return await _valVenalEstDbContext.Places.Include(p => p.Zone).ToListAsync();
        }                 
        public async Task<IActionResult> DeletePlace(long id)
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
        public async Task<IEnumerable<Place>> GetPlacesByZoneId(long IdZone)
        {
             return await _valVenalEstDbContext.Places
                            .Where(o => o.ZoneId == IdZone)
                            .ToListAsync();
        }
        public async void LoadDataInDbWithCsvFile(string accessPath)
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
        public async void SaveChange()
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