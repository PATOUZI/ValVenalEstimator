using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using ValVenalEstimator.Api.Contracts;
using ValVenalEstimator.Api.Data;
using ValVenalEstimator.Api.ViewModels;
using ValVenalEstimator.Api.Models;
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
        public async Task<Zone> AddAsyncZone(ZoneDTO zoneDTO)
        {
            var prefecture = await _iprefectureRepository.GetAsyncPrefecture(zoneDTO.PrefectureId);
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
        public async Task<Zone> GetAsyncZone(long id)
        {
            var zone = await _valVenalEstDbContext.Zones.FindAsync(id);

            if (zone == null)
            {
                return null;
            }
            return zone;
        }
        public async Task<IEnumerable<Zone>> GetAsyncAllZones()
        {
            return await _valVenalEstDbContext.Zones.Include(z => z.Prefecture).ToListAsync();
        }
         public async Task<IEnumerable<Zone>> GetAsyncAllZonesByPrefectureId(long idPrefecture)
        {
            return await _valVenalEstDbContext.Zones.Where(z => z.PrefectureId == idPrefecture).ToListAsync();
        }
        public async Task<IActionResult> DeleteAsyncZone(long id)
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
        public async void LoadAsyncDataInDbWithCsvFile(string accessPath)
        {
            using (var reader = new StreamReader(accessPath))   
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<ZoneDTO>();
            
                foreach (var z in records)
                {
                    await AddAsyncZone(z);
                
                }
            }
        }
        public async void SaveAsyncChange()
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