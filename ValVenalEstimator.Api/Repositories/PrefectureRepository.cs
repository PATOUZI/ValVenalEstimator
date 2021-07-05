using System;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using ValVenalEstimator.Api.Models;
using ValVenalEstimator.Api.Contracts;
using ValVenalEstimator.Api.Data;
using ValVenalEstimator.Api.ViewModels;

namespace ValVenalEstimator.Api.Repositories
{
    public class PrefectureRepository : IPrefectureRepository
    {
        readonly ValVenalEstimatorDbContext _valVenalEstDbContext;                  
        public PrefectureRepository(ValVenalEstimatorDbContext context)
        {  
            _valVenalEstDbContext = context;  
        } 
        public async Task<Prefecture> AddPrefectureAsync(Prefecture prefecture)
        {
            _valVenalEstDbContext.Add(prefecture);
            await _valVenalEstDbContext.SaveChangesAsync(); //SaveChangeAsync()
            return prefecture;
        }
        public async Task<Prefecture> GetPrefectureAsync(long id)
        {
            var prefecture = await _valVenalEstDbContext.Prefectures.FindAsync(id);
            if (prefecture == null)
            {
                throw new Exception("La zone avec l'id "+id+" n'existe pas !!!");
            }
            return prefecture;
        }
        public async Task<IEnumerable<Prefecture>> GetAllPrefecturesAsync()
        {
            return await _valVenalEstDbContext.Prefectures.ToListAsync();
        }
        public async Task<IActionResult> DeletePrefectureAsync(long id)
        {
            var prefecture = await _valVenalEstDbContext.Prefectures.FindAsync(id); //GetPrefectureAsync(id)
            if (prefecture == null)
            {
                return null;     //throw new Exception("La zone avec l'id "+id+" n'existe pas !!!"); 
            }     
            _valVenalEstDbContext.Prefectures.Remove(prefecture);                                      
            await _valVenalEstDbContext.SaveChangesAsync(); //SaveChangeAsync()
            return null;
        }
        public async void LoadDataInDbWithCsvFileAsync(string accessPath)
        {
            using (var reader = new StreamReader(accessPath))   
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<PrefectureDTO>();
                foreach (var p in records)
                {
                    Prefecture prefecture = new Prefecture();
                    prefecture.Name = p.Name;
                    await AddPrefectureAsync(prefecture);
                }
            }
        }
        public async void SaveChangeAsync()
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