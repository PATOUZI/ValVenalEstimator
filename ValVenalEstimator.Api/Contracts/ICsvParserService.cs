using System.Collections.Generic;
using ValVenalEstimator.Api.Models;

namespace ValVenalEstimator.Api.Contracts
{
    public interface ICsvParserService
    {
        List<PrefectureModel> ReadCsvFileTopPrefectureModel(string path);
    }
}