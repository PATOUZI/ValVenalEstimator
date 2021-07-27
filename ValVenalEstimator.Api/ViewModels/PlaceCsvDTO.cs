namespace ValVenalEstimator.Api.ViewModels
{
    public class PlaceCsvDTO
    {
        public string Name { get; set; }
        public long ZoneId { get; set; }
         public PlaceDTO ToPlaceDTO()
        {
            return new PlaceDTO(){
                Name = Name,
                ZoneId = ZoneId
            };
        }
    }
}