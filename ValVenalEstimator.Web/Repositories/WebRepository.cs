using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ValVenalEstimator.Web.Models;
using ValVenalEstimator.Web.Contracts;

namespace ValVenalEstimator.Web.Repositories
{
    public class WebRepository : IWebRepository 
    {
        public async Task<List<Prefecture>> Index()
        {
            List<Prefecture> prefectureList = new List<Prefecture>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:5004/api/Prefectures"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    prefectureList = JsonConvert.DeserializeObject<List<Prefecture>>(apiResponse);
                }
            }
            return prefectureList;
        }
        public async Task<bool> PlaceUploadFile(IFormFile file)
        {
            string path = "";
            bool iscopied = false;
            string extension = Path.GetExtension(file.FileName);
            if (extension == ".csv" && file.Length > 0)
            {
                string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "PlaceFileUpload")); //
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                    iscopied = true;
                }     
                string filePath = Path.Combine(path, filename);
                using (var httpClient = new HttpClient())
                {
                    string accessPath = @"https://localhost:5004/api/Places/LoadDataInDataBase?accessPath=" + filePath; //
                    var stringContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("field1", "value1"),
                        new KeyValuePair<string, string>("field2", "value2"),
                    });
                    await httpClient.PostAsync(accessPath, stringContent);
                }
            }
            else
            {
                iscopied = false;
            }
            return iscopied;
        }
        public async Task<bool> PrefectureUploadFile(IFormFile file)
        {
            string path = "";    
            bool iscopied = false;
            string extension = Path.GetExtension(file.FileName);
            if (extension == ".csv" && file.Length > 0)
            {
                string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "PrefectureFileUpload")); //
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                    iscopied = true;
                }
                string filePath = Path.Combine(path, filename);
                using (var httpClient = new HttpClient())
                {
                    string accessPath = @"https://localhost:5004/api/Prefectures/LoadDataInDataBase?accessPath=" + filePath; //
                    var stringContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("field1", "value1"),
                        new KeyValuePair<string, string>("field2", "value2"),
                    });
                        await httpClient.PostAsync(accessPath, stringContent);
                }
            }
            else
            {
                iscopied = false;
            }
            return iscopied;
        }
        public async Task<bool> ZoneUploadFile(IFormFile file)
        {
            string path = "";
            bool iscopied = false;
            string extension = Path.GetExtension(file.FileName);
            if (extension == ".csv" && file.Length > 0)
            {
                string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "ZoneFileUpload")); //
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                    iscopied = true;
                }
                string filePath = Path.Combine(path, filename);
                using (var httpClient = new HttpClient())
                {
                    string accessPath = @"https://localhost:5004/api/Zones/LoadDataInDataBase?accessPath=" + filePath; //
                    var stringContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("field1", "value1"),
                        new KeyValuePair<string, string>("field2", "value2"),
                    });
                        await httpClient.PostAsync(accessPath, stringContent);
                }
            }
            else
            {
                iscopied = false;
            }
            return iscopied;
        }
        public async Task<bool> UploadFile(IFormFile file, string directory, string accessPath)
        {
            string path = "";
            bool iscopied = false;
            string extension = Path.GetExtension(file.FileName);
            if (extension == ".csv" && file.Length > 0)
            {
                string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), directory));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                    iscopied = true;
                }
                string filePath = Path.Combine(path, filename);
                using (var httpClient = new HttpClient())
                {
                    string accessPath2 = accessPath + filePath;
                    var stringContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("field1", "value1"),
                        new KeyValuePair<string, string>("field2", "value2"),
                    });
                        await httpClient.PostAsync(accessPath2, stringContent);
                }
            }
            else
            {
                iscopied = false;
            }
            return iscopied;
        }
    }
}