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
        public Task<IEnumerable<Prefecture>> GetAllPrefecturesAsync();           
        //public Task<IEnumerable<Prefecture>> GetAllPrefecturesWithZonesAsync();
        public void LoadDataInDbWithCsvFileAsync(string accessPath);
        public void SaveChangeAsync();
        public bool PrefectureExists(long id);
        public void Remove(Prefecture prefecture);
    }
}