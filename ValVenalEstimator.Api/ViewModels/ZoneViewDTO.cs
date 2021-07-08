using ValVenalEstimator.Api.Enums;
using ValVenalEstimator.Api.Models;

namespace ValVenalEstimator.Api.ViewModels
{
    public class ZoneViewDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int ZoneNum { get; set; }
        public string  Type { get; set; }
        public double PricePerMeterSquare { get; set; }
        public long PrefectureId { get; set; }
    }
}