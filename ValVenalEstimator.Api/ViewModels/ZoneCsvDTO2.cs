using ValVenalEstimator.Api.Enums;
using ValVenalEstimator.Api.Models;

namespace ValVenalEstimator.Api.ViewModels
{
    public class ZoneCsvDTO2
    {
        public string Name { get; set; }
        public int ZoneNum { get; set; }
        public ZoneType  Type { get; set; }
        public double PricePerMeterSquare { get; set; }
        public string PrefectureName { get; set; }
        public Zone ToZone()
        {
            return new Zone(){
                Name = Name,
                ZoneNum = ZoneNum,
                Type = Type,
                PricePerMeterSquare = PricePerMeterSquare
            };
        }
   
    }
}