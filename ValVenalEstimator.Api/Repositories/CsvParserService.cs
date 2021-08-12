  
using CsvHelper;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using ValVenalEstimator.Api.Mappers;
using ValVenalEstimator.Api.Models;
using ValVenalEstimator.Api.Contracts;

namespace ValVenalEstimator.Api.Repositories
{
    public class CsvParserService : ICsvParserService
    {
        public List<PrefectureModel> ReadCsvFileTopPrefectureModel(string path)
        {
            try
                {
                    using (var reader = new StreamReader(path, Encoding.Default))
                    using (var csv = new CsvReader(reader))
                    {
                        csv.Configuration.RegisterClassMap<PrefectureMap>();
                        var records = csv.GetRecords<PrefectureModel>().ToList();
                        return records;
                    }
                }
                catch (UnauthorizedAccessException e)
                {
                    throw new Exception(e.Message);
                }
                catch (FieldValidationException e)
                {
                    throw new Exception(e.Message);
                }
                catch (CsvHelperException e)
                {
                    throw new Exception(e.Message);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
        }
    }
}