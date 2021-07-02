using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ValVenalEstimator.Api.Models; 
namespace ValVenalEstimator.Api.Contracts
{
    public interface IPrefectureRepository
    {
        public Task<Prefecture> AddAsyncPrefecture(Prefecture prefecture);
        public Task<Prefecture> GetAsyncPrefecture(long id);
        public Task<IEnumerable<Prefecture>> GetAsyncAllPrefectures();     
        // public Task<IEnumerable<Prefecture>> GetAllPrefecturesWithZones();
        public Task<IActionResult> DeleteAsyncPrefecture(long id);
        public void LoadAsyncDataInDbWithCsvFile(string accessPath);
        public void SaveAsyncChange();
        public bool PrefectureExists(long id);
        public void Remove(Prefecture prefecture);
    }
}