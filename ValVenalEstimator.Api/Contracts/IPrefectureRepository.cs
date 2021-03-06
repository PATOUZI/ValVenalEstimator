using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ValVenalEstimator.Api.Models; 
using ValVenalEstimator.Api.ViewModels; 

namespace ValVenalEstimator.Api.Contracts
{
    public interface IPrefectureRepository
    {
        public Task<Prefecture> AddPrefectureAsync(PrefectureDTO prefectureDTO);
        public Task<Prefecture> GetPrefectureAsync(long id);       
        public Task<Prefecture> GetPrefectureByNameAsync(string name);
        public Task<IEnumerable<Prefecture>> GetAllPrefecturesAsync();           
        //public Task<List<Prefecture>> GetAllPrefecturesWithZonesAsync();       
        public void LoadDataInDbWithCsvFile(string accessPath);
        public void SaveChanges();
        public bool PrefectureExists(long id);     
        public void Remove(Prefecture prefecture);
    }
}