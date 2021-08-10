using ValVenalEstimator.Api.Models;
namespace ValVenalEstimator.Api.ViewModels
{
    public class PlaceCsv2DTO
    {
        public string Name { get; set; }
        public string ZoneName { get; set; }
        public string PrefectureName { get; set; }
        public Place ToPlace()
        {
            return new Place(){
                Name = Name
            };
        }
    }
}