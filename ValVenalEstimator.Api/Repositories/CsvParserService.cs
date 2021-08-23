using CsvHelper;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using ValVenalEstimator.Api.Mappers;
using ValVenalEstimator.Api.Models;
using ValVenalEstimator.Api.Contracts;
using ValVenalEstimator.Api.ViewModels;

namespace ValVenalEstimator.Api.Repositories
{
    public class CsvParserService : ICsvParserService
    {
        readonly IPrefectureRepository _iPrefectureRepository; 
        public CsvParserService(IPrefectureRepository iPrefectureRepository)
        {
            _iPrefectureRepository = iPrefectureRepository;
        }
        //public List<PrefectureModel> ReadCsvFileToPrefectureModel(string path)
        /*public async void ReadCsvFileToPrefectureModel(string path)
        {
            try
            {
                using (var reader = new StreamReader(path, Encoding.Default))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.RegisterClassMap<PrefectureMap>();
                    var records = csv.GetRecords<PrefectureModel>().ToList();
                    foreach (var p in records)
                    {
                        PrefectureDTO prefectDTO = p.ToPrefectureDTO();
                        await _iPrefectureRepository.AddPrefectureAsync(prefectDTO);    
                    } 

                    //return records;
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
                //throw new Exception("Il a un problème avec votre fichier");
                throw new Exception(e.Message);   
            }
        }*/
    }
}