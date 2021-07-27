using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ValVenalEstimator.Api.Models; 
using ValVenalEstimator.Api.ViewModels; 

namespace ValVenalEstimator.Api.Contracts
{
    public interface IPlaceRepository
    {
        public Task<Place> AddPlaceAsync(PlaceDTO placeDTO);
        public Task<Place> GetPlaceAsync(long id);
        public Task<PlaceViewDTO> GetPlaceViewDTOAsync(long id);
        public Task<IEnumerable<Place>> GetAllPlacesAsync();
        public void LoadDataInDbWithCsvFileAsync(string accessPath);     
        public Task<IEnumerable<Place>> GetPlacesByPrefectureIdAsync(long idPrefecture);
        public Task<IEnumerable<Place>> GetPlacesByZoneIdAsync(long idZone);
        public void SaveChangeAsync();
        public bool PlaceExists(long id);
        public void Remove(Place place);
    }

}