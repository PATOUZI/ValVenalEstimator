using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ValVenalEstimator.Api.Models; 

namespace ValVenalEstimator.Api.Contracts
{
    public interface IPlaceRepository
    {
        public Task<Place> AddPlaceAsync(Place place);
        public Task<Place> GetPlaceAsync(long id);
        public Task<IEnumerable<Place>> GetAllPlacesAsync();
        public Task<IActionResult> DeletePlaceAsync(long id);
        public void LoadDataInDbWithCsvFileAsync(string accessPath);     
        public Task<IEnumerable<Place>> GetPlacesByPrefectureId(long idPrefecture);
        public Task<IEnumerable<Place>> GetPlacesByZoneIdAsync(long idZone);
        public void SaveChangeAsync();
        public bool PlaceExists(long id);
        public void Remove(Place place);
    }

}