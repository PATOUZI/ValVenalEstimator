using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ValVenalEstimator.Api.Models; 
namespace ValVenalEstimator.Api.Contracts
{
    public interface IPlaceRepository
    {
        public Task<Place> AddAsyncPlace(Place place);
        public Task<Place> GetAsyncPlace(long id);
        public Task<IEnumerable<Place>> GetAsyncAllPlaces();
        public Task<IActionResult> DeleteAsyncPlace(long id);
        public void LoadAsyncDataInDbWithCsvFile(string accessPath);
        //public Task<IEnumerable<Place>> GetPlacesByPrefectureId(long IdPrefecture);
        public Task<IEnumerable<Place>> GetAsyncPlacesByZoneId(long IdZone);
        public void SaveAsyncChange();
        public bool PlaceExists(long id);
        public void Remove(Place place);
    }

}