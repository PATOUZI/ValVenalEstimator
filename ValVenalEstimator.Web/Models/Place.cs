using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ValVenalEstimator.Web.Models
{
    public class Place
    {
        public long Id { get; set; }
        public string Prefecture { get; set; }
        public string District { get; set; }
        public double PricePerMeterSquare { get; set; }
    }
}
