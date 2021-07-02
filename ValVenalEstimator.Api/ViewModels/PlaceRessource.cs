namespace ValVenalEstimator.Api.ViewModels
{
    public class PlaceRessource
    {
        public long Id { get; set; }
        public string District { get; set; }
        public double PricePerMeterSquare { get; set; }
        public PrefectureDTO Prefecture { get; set; }
    }
}