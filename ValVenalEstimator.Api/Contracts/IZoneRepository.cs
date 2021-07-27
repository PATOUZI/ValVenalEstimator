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
        public Task<Zone> GetZoneAsync(long id);
        public Task<IEnumerable<Zone>> GetAllZonesAsync();
        public Task<IEnumerable<Zone>> GetAllZonesByPrefectureIdAsync(long prefectureId);
        public void LoadDataInDbWithCsvFileAsync(string accessPath);
        public  void LoadData(string accessPath);
        public void SaveChangeAsync();
        public bool ZoneExists(long id);
        public void Remove(Zone Zone);
    }
}