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
using ValVenalEstimator.Api.Data;
using ValVenalEstimator.Api.Models;
using ValVenalEstimator.Api.Contracts;
using ValVenalEstimator.Api.ViewModels;

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
        public async Task<Zone> AddZoneAsync(ZoneDTO zoneDTO)
        {
            var prefecture = await _iprefectureRepository.GetPrefectureAsync(zoneDTO.PrefectureId);
            if (prefecture != null)
            {
                Zone zone = zoneDTO.ToZone();
                zone.Prefecture = prefecture;
                zone.Code = prefecture.Name + "_" + zoneDTO.Name;
                await _valVenalEstDbContext.AddAsync(zone);
                //await _valVenalEstDbContext.SaveChangesAsync(); 
                SaveChangeAsync();
                return zone;
            } 
            else
            {
                throw new Exception("La prefecture avec l'id "+zoneDTO.PrefectureId+" n'existe pas !!!");
            }  
        }
        public async Task<Zone> AddZoneAsync2(ZoneCsvDTO2 zoneCsvDTO)
        {           
            var prefecture = await _iprefectureRepository.GetPrefectureByNameAsync(zoneCsvDTO.PrefectureName);
            if (prefecture != null)
            {
                Zone zone = zoneCsvDTO.ToZone();
                zone.Prefecture = prefecture;
                zone.Code = prefecture.Name + "_" + zoneCsvDTO.Name;
                await _valVenalEstDbContext.AddAsync(zone);
                //await _valVenalEstDbContext.SaveChangesAsync(); 
                SaveChangeAsync();
                return zone;
            } 
            else
            {
                throw new Exception("La prefecture avec le nom "+zoneCsvDTO.PrefectureName+" n'existe pas !!!");
            }  
        }
        public async Task<Zone> GetZoneAsync(long id)
        {
            var zone = await _valVenalEstDbContext.Zones.FindAsync(id);
            if (zone == null)
            {
                throw new Exception("La zone avec l'id "+id+" n'existe pas !!!");
            }
            return zone;
        }    
        public async Task<Zone> GetZoneByZoneNameAndPrefectureNameAsync(string zoneName, string prefectName)
        {      
            var prefecture = await _iprefectureRepository.GetPrefectureByNameAsync(prefectName);
            if (prefecture != null)
            {
                long idPref = prefecture.Id;
                var zone = await _valVenalEstDbContext.Zones.Where(z => (z.Name == zoneName && z.PrefectureId == idPref)).SingleOrDefaultAsync();
                if (zone != null)
                {
                    return zone;
                }
                else
                {
                    throw new Exception("La zone avec pour nom de préfecture "+prefectName+ " et pour nom de zone "+zoneName+" n'existe pas !!!");                    
                }
            }
            return null;
        }
        public async Task<IEnumerable<Zone>> GetAllZonesAsync()
        {
            return await _valVenalEstDbContext.Zones.Include(z => z.Prefecture).ToListAsync();
        }
        public async Task<IEnumerable<Zone>> GetAllZonesByPrefectureIdAsync(long idPrefecture)
        {
            return await _valVenalEstDbContext.Zones.Where(z => z.PrefectureId == idPrefecture).ToListAsync();
        }
        public async void LoadDataInDbWithCsvFileAsync(string accessPath)
        {
            using (var reader = new StreamReader(accessPath))   
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<ZoneCsvDTO>();           
                foreach (var z in records)
                {
                    ZoneDTO zoneDTO = z.ToZoneDTO(); 
                    await AddZoneAsync(zoneDTO);               
                }
            }
        }
        public async void LoadData(string accessPath)
        {
            using (var reader = new StreamReader(accessPath))   
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<ZoneCsvDTO2>();           
                foreach (var z in records)
                {
                    await AddZoneAsync2(z);               
                }
            }
        }
        public async void SaveChangeAsync()
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