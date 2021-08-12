using CsvHelper.Configuration;
using ValVenalEstimator.Api.Models;

namespace ValVenalEstimator.Api.Mappers
{
    public sealed class PrefectureMap : ClassMap<PrefectureModel>
    {
        public PrefectureMap()
        {
            Map(m => m.Name).Name(Constants.CsvHeaders.Name);
        }
    }
}