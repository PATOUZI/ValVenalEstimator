using ValVenalEstimator.Api.Models;

namespace ValVenalEstimator.Api.ViewModels
{
    public class PlaceViewDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ZoneViewDTO Zone { get; set; }
    }
}