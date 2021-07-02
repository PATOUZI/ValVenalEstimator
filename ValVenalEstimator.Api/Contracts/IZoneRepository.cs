using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ValVenalEstimator.Api.Models; 
using ValVenalEstimator.Api.ViewModels;
namespace ValVenalEstimator.Api.Contracts
{
    public interface IZoneRepository
    {
        //public Task<Zone> AddZone(Zone Zone);
        public Task<Zone> AddAsyncZone(ZoneDTO ZoneDTO);
        public Task<Zone> GetAsyncZone(long id);
        public Task<IEnumerable<Zone>> GetAsyncAllZones();
        public Task<IEnumerable<Zone>> GetAsyncAllZonesByPrefectureId(long prefectureId);

        public Task<IActionResult> DeleteAsyncZone(long id);
        public void LoadAsyncDataInDbWithCsvFile(string accessPath);
        public void SaveAsyncChange();
        public bool ZoneExists(long id);
        public void Remove(Zone Zone);
    }
}