using CsvHelper.Configuration.Attributes;
using ValVenalEstimator.Api.ViewModels;
namespace ValVenalEstimator.Api.Models
{
    public class PrefectureModel
    {
        [Name(Constants.CsvHeaders.Name)]
        public string Name { get; set; }
        public PrefectureDTO ToPrefectureDTO()
        {
             return new PrefectureDTO(){
                Name = Name
            };
        }
    }
}      