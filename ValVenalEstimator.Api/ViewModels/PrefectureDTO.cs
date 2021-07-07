using ValVenalEstimator.Api.Models;

namespace ValVenalEstimator.Api.ViewModels
{
    public class PrefectureDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Prefecture ToPrefecture()
        {
            return new Prefecture(){
                Id = Id,
                Name = Name
            };
        }
    }
}