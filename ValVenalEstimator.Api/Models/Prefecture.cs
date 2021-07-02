using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ValVenalEstimator.Api.Models
{
    public class Prefecture
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Zone> Zones { get; set; }
    }
}