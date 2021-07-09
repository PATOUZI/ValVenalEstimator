using System;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ValVenalEstimator.Web.Models;
using ValVenalEstimator.Web.ViewModels;

namespace ValVenalEstimator.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Prefecture> PrefectureList = new List<Prefecture>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:5004/api/Prefectures"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    PrefectureList = JsonConvert.DeserializeObject<List<Prefecture>>(apiResponse);
                }
            }
            return View(PrefectureList);
        }
        public IActionResult FileUpload()
        {
            return View();
        }
        public IActionResult PrefectureFileUpload()
        {
            return View();
        }
        public IActionResult ZoneFileUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> FileUpload(IFormFile file)
        {
            await UploadFile(file);
            TempData["msg"] = "File Uploaded successfully.";
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PrefectureFileUpload(IFormFile file)
        {
            await PrefectureUploadFile(file);
            TempData["msg"] = "File Uploaded successfully.";
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> ZoneFileUpload(IFormFile file)
        {
            await ZoneUploadFile(file);
            TempData["msg"] = "File Uploaded successfully.";
            return View();
        }   

        [HttpPost]        
        public async Task<IActionResult> GetValVenal(long idPlace, int area, long prefect)
        {
            //Calcul de la valeur venale du terrain
            string accessPath = @"https://localhost:5004/api/Places/" + idPlace + "/" + area ;
            ValVenalDTO ValVenalDTO = new ValVenalDTO();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(accessPath))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ValVenalDTO = JsonConvert.DeserializeObject<ValVenalDTO>(apiResponse); 
  
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
            }
            //Recuperation de l'objet Prefecture correspondant à la Place dont l'id est fournie
            string accessPath2 = @"https://localhost:5004/api/Prefectures/" + prefect ;
            Prefecture prefecture = new Prefecture();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(accessPath2))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        prefecture = JsonConvert.DeserializeObject<Prefecture>(apiResponse);   
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }
            ValVenalDTO.PrefectureName = prefecture.Name;
            ValVenalDTO.Area = area;
            return View(ValVenalDTO);
        }
        public async Task<bool> UploadFile(IFormFile file)
        {
            string path = "";
            bool iscopied = false;
            if (file.Length > 0)
            {
                string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Upload"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                    iscopied = true;
                }
                string filePath = Path.Combine(path, filename);
                using (var httpClient = new HttpClient())
                {
                    string accessPath = @"https://localhost:5004/api/Places/LoadDataInDataBase?accessPath=" + filePath;
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
            if (file.Length > 0)
            {
                string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Upload"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                    iscopied = true;
                }
                string filePath = Path.Combine(path, filename);
                using (var httpClient = new HttpClient())
                {
                    string accessPath = @"https://localhost:5004/api/Prefectures/LoadDataInDataBase?accessPath=" + filePath;
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
            if (file.Length > 0)
            {
                string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Upload"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                    iscopied = true;
                }
                string filePath = Path.Combine(path, filename);
                using (var httpClient = new HttpClient())
                {
                    string accessPath = @"https://localhost:5004/api/Zones/LoadDataInDataBase?accessPath=" + filePath;
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
    }
}
     