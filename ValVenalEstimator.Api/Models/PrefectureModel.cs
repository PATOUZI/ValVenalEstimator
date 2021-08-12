using CsvHelper.Configuration.Attributes;

namespace ValVenalEstimator.Api.Models
{
    public class PrefectureModel
    {
        [Name(Constants.CsvHeaders.Name)]
        public string Name { get; set; }
    }
}      