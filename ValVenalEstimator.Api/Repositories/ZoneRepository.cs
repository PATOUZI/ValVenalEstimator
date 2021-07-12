using AutoMapper;
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
        public async Task<Zone> GetZoneAsync(long id)
        {
            var zone = await _valVenalEstDbContext.Zones.FindAsync(id);
            if (zone == null)
            {
                throw new Exception("La zone avec l'id "+id+" n'existe pas !!!");
            }
            return zone;
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