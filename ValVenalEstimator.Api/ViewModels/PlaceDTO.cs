using ValVenalEstimator.Api.Models;

namespace ValVenalEstimator.Api.ViewModels
{
    public class PlaceDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ZoneId { get; set; }
         public Place ToPlace()
        {
            return new Place(){
                Id = Id,
                Name = Name,
                ZoneId = ZoneId
            };
        }
    }
}