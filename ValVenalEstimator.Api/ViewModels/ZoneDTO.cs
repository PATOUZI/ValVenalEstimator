using ValVenalEstimator.Api.Enums;
using ValVenalEstimator.Api.Models;

namespace ValVenalEstimator.Api.ViewModels
{
    public class ZoneDTO
    {
        public string Name { get; set; }
        public int ZoneNum { get; set; }
        public ZoneType  Type { get; set; }
        public double PricePerMeterSquare { get; set; }
        public long PrefectureId { get; set; }

        public Zone ToZone()
        {
            return new Zone(){
                Name = Name,
                ZoneNum = ZoneNum,
                Type = Type,
                PricePerMeterSquare = PricePerMeterSquare,
                PrefectureId = PrefectureId
            };
        }
   
    }
}