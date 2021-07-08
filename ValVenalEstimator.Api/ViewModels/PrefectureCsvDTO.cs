using ValVenalEstimator.Api.ViewModels;

namespace ValVenalEstimator.Api.ViewModels
{
    public class PrefectureCsvDTO
    {
        public string Name { get; set; }
        
        public PrefectureDTO ToPrefectureDTO()
        {
            return new PrefectureDTO(){
                Name = Name
            };
        }
    }
}