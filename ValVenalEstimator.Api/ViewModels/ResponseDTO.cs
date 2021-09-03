namespace ValVenalEstimator.Api.ViewModels
{
    public class ResponseDTO
    {
        public double ValVenal { get; set; }
        public double ValEnregistrement { get; set; }
        public double DroitDeTimbre { get; set; }
        public double PriceOfBornageContradictoire { get; set; }
        public double CalculationBasis { get; set; }
        public double PriceToPay { get; set; }
        public string ZoneName { get; set; }
        public string ZoneType { get; set; }
        public string PlaceName { get; set; }
        public int Area { get; set; }
    }
}