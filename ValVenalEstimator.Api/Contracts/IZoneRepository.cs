using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ValVenalEstimator.Api.Models; 
using ValVenalEstimator.Api.ViewModels;

namespace ValVenalEstimator.Api.Contracts
{
    public interface IZoneRepository
    {
        public Task<Zone> AddZoneAsync(ZoneDTO zoneDTO);
        public Task<Zone> AddZoneAsync2(ZoneCsvDTO2 zoneCsvDTO);
        public Task<Zone> GetZoneAsync(long id);
        public Task<Zone> GetZoneByZoneNameAndPrefectureNameAsync(string zoneName, string prefectName);        
        public Task<IEnumerable<Zone>> GetAllZonesAsync();
        public Task<IEnumerable<Zone>> GetAllZonesByPrefectureIdAsync(long prefectureId);
        public void LoadDataInDbWithCsvFile(string accessPath);
        public void LoadData(string accessPath);
        public void SaveChange();
        public bool ZoneExists(long id);
        public void Remove(Zone Zone);
    }
}