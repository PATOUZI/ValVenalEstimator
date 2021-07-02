using ValVenalEstimator.Api.Contracts;
using ValVenalEstimator.Api.Data;
using ValVenalEstimator.Api.ViewModels;
using System.Threading.Tasks;
using ValVenalEstimator.Api.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using System;

namespace ValVenalEstimator.Api.Repositories
{
    public class ZoneRepository : IZoneRepository
    {
        readonly ValVenalEstimatorDbContext _valVenalEstDbContext; 
        readonly IPrefectureRepository _iprefectureRepository;
        
        public ZoneRepository(ValVenalEstimatorDbContext context, IPrefectureRepository iprefectureRepository)
        {  
            _valVenalEstDbContext = context;   
            _iprefectureRepository = iprefectureRepository;
        } 

        /*public async Task<Zone> AddZone(Zone zone)
        {
            var existingPrefecture = _iprefectureRepository.PrefectureExists(zone.PrefectureId);
            if (existingPrefecture == true)
            {
                var prefecture = _iprefectureRepository.GetPrefecture(zone.PrefectureId);
                zone.Prefecture = prefecture.Result;
                _valVenalEstDbContext.Add(zone);
                await _valVenalEstDbContext.SaveChangesAsync();
                return zone;
            } 
            else
            {
                return null;
            }  
        }*/

        public async Task<Zone> AddZone(ZoneDTO zoneDTO)
        {
            var prefecture = await _iprefectureRepository.GetPrefecture(zoneDTO.PrefectureId);
            if (prefecture != null)
            {
                Zone zone = zoneDTO.ToZone();
                zone.Prefecture = prefecture;
                zone.Code = prefecture.Name + "_" + zoneDTO.Name;
                await _valVenalEstDbContext.AddAsync(zone);
                await _valVenalEstDbContext.SaveChangesAsync();
                return zone;
            } 
            else
            {
                throw new Exception("La prefecture avec l'id "+zoneDTO.PrefectureId+" n'existe pas !!!");
            }  
        }

        public async Task<Zone> GetZone(long id)
        {
            var zone = await _valVenalEstDbContext.Zones.FindAsync(id);

            if (zone == null)
            {
                return null;
            }
            return zone;
        }

        public async Task<IEnumerable<Zone>> GetAllZones()
        {
            return await _valVenalEstDbContext.Zones.Include(z => z.Prefecture).ToListAsync();
        }

         public async Task<IEnumerable<Zone>> GetAllZonesByPrefectureId(long idPrefecture)
        {
            return await _valVenalEstDbContext.Zones.Where(z => z.PrefectureId == idPrefecture).ToListAsync();
        }

        public async Task<IActionResult> DeleteZone(long id)
        {
            var zone = await _valVenalEstDbContext.Zones.FindAsync(id);

            if (zone == null)
            {
                return null;     
            }     
            _valVenalEstDbContext.Zones.Remove(zone);                                      
            await _valVenalEstDbContext.SaveChangesAsync();
            return null;
        }

        public async void LoadDataInDbWithCsvFile(string accessPath)
        {
            using (var reader = new StreamReader(accessPath))   
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<ZoneDTO>();
            
                foreach (var z in records)
                {
                    await AddZone(z);
                
                }
            }
        }
        

        public async void SaveChange()
        {
            await _valVenalEstDbContext.SaveChangesAsync();
        }

        public bool ZoneExists(long id) => _valVenalEstDbContext.Zones.Any(z => z.Id == id);
        
        public void Remove(Zone zone)
        {
            _valVenalEstDbContext.Zones.Remove(zone);   
        }
    }   
}