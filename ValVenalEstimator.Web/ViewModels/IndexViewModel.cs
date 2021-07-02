using System.Collections.Generic;
using ValVenalEstimator.Web.Models;

namespace ValVenalEstimator.Web.ViewModels
{
    public class IndexViewModel
    {
        public List<Place> PlaceList { get; set; }
        public List<string> PrefectureList { get; set; }
    }
}