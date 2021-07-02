using ValVenalEstimator.Api.Contracts;
using ValVenalEstimator.Api.Data;
using ValVenalEstimator.Api.ViewModels;
using System.Threading.Tasks;
using ValVenalEstimator.Api.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;

namespace ValVenalEstimator.Api.Repositories
{
    public class PrefectureRepository : IPrefectureRepository
    {
        readonly ValVenalEstimatorDbContext _valVenalEstDbContext;   
                
        public PrefectureRepository(ValVenalEstimatorDbContext context)
        {  
            _valVenalEstDbContext = context;  
        } 
        public async Task<Prefecture> AddPrefecture(Prefecture prefecture)
        {
            _valVenalEstDbContext.Add(prefecture);
            await _valVenalEstDbContext.SaveChangesAsync();
            return prefecture;
        }
        public async Task<Prefecture> GetPrefecture(long id)
        {
            var prefecture = await _valVenalEstDbContext.Prefectures.FindAsync(id);

            if (prefecture == null)
            {
                return null;
            }
            return prefecture;
        }

        public async Task<IEnumerable<Prefecture>> GetAllPrefectures()
        {
            return await _valVenalEstDbContext.Prefectures.ToListAsync();
        }

       

        public async Task<IActionResult> DeletePrefecture(long id)
        {
            var prefecture = await _valVenalEstDbContext.Prefectures.FindAsync(id);

            if (prefecture == null)
            {
                return null;     
            }     
            _valVenalEstDbContext.Prefectures.Remove(prefecture);                                      
            await _valVenalEstDbContext.SaveChangesAsync();
            return null;
        }

        public async void LoadDataInDbWithCsvFile(string accessPath)
        {
            using (var reader = new StreamReader(accessPath))   
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<PrefectureDTO>();
                foreach (var p in records)
                {
                    Prefecture prefecture = new Prefecture();
                    prefecture.Name = p.Name;
                    _valVenalEstDbContext.Add<Prefecture>(prefecture);               
                    await _valVenalEstDbContext.SaveChangesAsync();
                }
            }
        }

        public async void SaveChange()
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