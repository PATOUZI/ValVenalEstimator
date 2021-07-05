using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ValVenalEstimator.Api.Enums;

namespace ValVenalEstimator.Api.Models
{
    public class Zone
    {
        [Key]
        public long Id { get; set; }     
        public string Name { get; set; }
        public int ZoneNum { get; set; }        
        
        // prefecture name concat zone name
        public string Code { get; set; }
        public ZoneType  Type { get; set; }
        public double PricePerMeterSquare { get; set; }
        public long PrefectureId { get; set; }        
        
        [JsonIgnore]
        public Prefecture Prefecture { get; set; }

        [JsonIgnore]       
        public List<Place> Places { get; set; }
    }
}