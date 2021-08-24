using CsvHelper;
using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValVenalEstimator.Api.Data;
using ValVenalEstimator.Api.Models;
using ValVenalEstimator.Api.Contracts;
using ValVenalEstimator.Api.ViewModels;

namespace ValVenalEstimator.Api.Repositories
{
    public class PrefectureRepository : IPrefectureRepository
    {
        readonly ValVenalEstimatorDbContext _valVenalEstDbContext;

        /*readonly IZoneRepository _iZoneRepository; 
        public PrefectureRepository(ValVenalEstimatorDbContext context, IZoneRepository iZoneRepository)
        {  
            _valVenalEstDbContext = context;  
            _iZoneRepository = iZoneRepository;
        }*/
        public PrefectureRepository(ValVenalEstimatorDbContext context)
        {
            _valVenalEstDbContext = context;
        }
        public async Task<Prefecture> AddPrefectureAsync(PrefectureDTO prefectureDTO)
        {
            Prefecture p = prefectureDTO.ToPrefecture();
            await _valVenalEstDbContext.AddAsync(p);
            SaveChanges();
            return p;
        }
        public async Task<Prefecture> GetPrefectureAsync(long id)
        {
            var prefecture = await _valVenalEstDbContext.Prefectures.FindAsync(id);
            if (prefecture == null)
            {
                throw new Exception("La préfecture avec l'id " + id + " n'existe pas !!!");
            }
            return prefecture;
        }
        public async Task<IEnumerable<Prefecture>> GetAllPrefecturesAsync()
        {
            var listPref = await _valVenalEstDbContext.Prefectures.OrderBy(p => p.Name).ToListAsync();
            return listPref;
        }
        public async Task<Prefecture> GetPrefectureByNameAsync(string name)
        {
            var prefecture = await _valVenalEstDbContext.Prefectures.Where(p => p.Name == name).SingleOrDefaultAsync();
            if (prefecture == null)
            {
                throw new Exception("La préfecture avec pour nom " + name + " n'existe pas !!!");
            }
            return prefecture;
        }

        /*public async Task<List<Prefecture>> GetAllPrefecturesWithZonesAsync()
        {
            var list  =  await GetAllPrefecturesAsync();
            var resList = list.Select( 
                                        async(p) => 
                                        {
                                            p.Zones = (await _iZoneRepository.GetAllZonesByPrefectureIdAsync(p.Id)).ToList();
                                            return p;
                                        }
                                     ).ToList();
            return resList;
        }*/
        
        /*Cette méthode permet d'enrégistrer des préfectures grace à un fichier csv 
        contenant les noms des prefectures avec gestion des exceptions. Mais la 
        gestion des exceptions ne marchent pas pour le moment*/
        public async void LoadDataInDbWithCsvFile(string accessPath)
        {
            try
            {
                using (var reader = new StreamReader(accessPath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<PrefectureCsvDTO>().ToList();
                    foreach (var p in records)
                    {
                        PrefectureDTO prefectDTO = p.ToPrefectureDTO();
                        await AddPrefectureAsync(prefectDTO);
                    }
                }
            }
            catch (BadDataException ex)
            {
                Console.WriteLine(ex.Message);
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
        public async void SaveChanges()
        {
            await _valVenalEstDbContext.SaveChangesAsync();
        }
        public bool PrefectureExists(long id) => _valVenalEstDbContext.Prefectures.Any(p => p.Id == id);
        public void Remove(Prefecture prefecture)
        {
            _valVenalEstDbContext.Prefectures.Remove(prefecture);
        }
    }
}