using System.ComponentModel.DataAnnotations;
namespace ValVenalEstimator.Api.Models
{
    public class Place
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public long ZoneId { get; set; }
        public Zone Zone { get; set; }
    }
}