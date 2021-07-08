using ValVenalEstimator.Api.Enums;

namespace ValVenalEstimator.Api.ViewModels
{
    public class ZoneCsvDTO
    {
        public string Name { get; set; }
        public int ZoneNum { get; set; }
        public ZoneType  Type { get; set; }
        public double PricePerMeterSquare { get; set; }
        public long PrefectureId { get; set; }
        public ZoneDTO ToZoneDTO()
        {
            return new ZoneDTO(){
                Name = Name,
                ZoneNum = ZoneNum,
                Type = Type,
                PricePerMeterSquare = PricePerMeterSquare,
                PrefectureId = PrefectureId
            };
        }
   
    }
}