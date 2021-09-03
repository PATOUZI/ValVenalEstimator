using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ValVenalEstimator.Web.Models;
using ValVenalEstimator.Web.Contracts;
using ValVenalEstimator.Web.ViewModels;

namespace ValVenalEstimator.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebRepository _iWebRepository;
        public HomeController(IWebRepository iWebRepository)
        {
            _iWebRepository = iWebRepository;
        }
        public async Task<IActionResult> Index()
        //public async Task<List<Prefecture>> Index()
        {
            /*List<Prefecture> prefectureList = new List<Prefecture>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:5004/api/Prefectures"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    prefectureList = JsonConvert.DeserializeObject<List<Prefecture>>(apiResponse);
                }
            }*/
            //return View(prefectureList);
            return View(await _iWebRepository.Index());
        }
        public IActionResult PlaceFileUpload()
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
        /*public async Task<bool> PlaceUploadFile(IFormFile file)
        {
            string path = "";
            bool iscopied = false;
            string extension = Path.GetExtension(file.FileName);
            if (extension == ".csv" && file.Length > 0)
            {
                string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "PlaceFileUpload"));
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
            string extension = Path.GetExtension(file.FileName);
            if (extension == ".csv" && file.Length > 0)
            {
                string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "PrefectureFileUpload"));
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
            string extension = Path.GetExtension(file.FileName);
            if (extension == ".csv" && file.Length > 0)
            {
                string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "ZoneFileUpload"));
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
        }*/

        [HttpPost]
        public async Task<ActionResult> PlaceFileUpload(IFormFile file)
        {
            //bool result = await PlaceUploadFile(file);
            //bool result = await _iWebRepository.PlaceUploadFile(file);
            string placeAccessPath = @"https://localhost:5004/api/Places/LoadDataInDataBase?accessPath=";
            string placeDirectory = "PlaceFileUpload";
            bool result = await _iWebRepository.UploadFile(file, placeDirectory, placeAccessPath);
            if (result)
            {
                TempData["msg"] = "File Uploaded successfully.";                
            }
            else
            {
                TempData["msg"] = "Le type de fichier ne correspond.";  
            }            
            return View();
        }     
    
        [HttpPost]        
        public async Task<ActionResult> PrefectureFileUpload(IFormFile file)
        {
            //bool result = await PrefectureUploadFile(file);
            //bool result = await _iWebRepository.PrefectureUploadFile(file);
            string prefectureAccessPath = @"https://localhost:5004/api/Prefectures/LoadDataInDataBase?accessPath=";
            string prefectureDirectory = "PrefectureFileUpload";
            bool result = await _iWebRepository.UploadFile(file, prefectureDirectory, prefectureAccessPath);
            if (result)
            {
                TempData["msg"] = "File Uploaded successfully.";                
            }
            else
            {
                TempData["msg"] = "Le type de fichier ne correspond.";  
            }            
            return View();
        }
            
        [HttpPost]
        public async Task<ActionResult> ZoneFileUpload(IFormFile file)
        {
            //bool result = await ZoneUploadFile(file);
            //bool result = await _iWebRepository.ZoneUploadFile(file);   
            string zoneAccessPath = @"https://localhost:5004/api/Zones/LoadDataInDataBase?accessPath=";
            string zoneDirectory = "ZoneFileUpload";
            bool result = await _iWebRepository.UploadFile(file, zoneDirectory, zoneAccessPath);
            if (result)
            {
                TempData["msg"] = "File Uploaded successfully.";                
            }
            else
            {
                TempData["msg"] = "Le type de fichier ne correspond.";  
            }
            return View();
        }   

        [HttpPost]        
        public async Task<IActionResult> GetValues(long idPlace, int hectare, int are, int centiare, long prefect, double valAchat, int nbrePge)  
        {
            int area = (hectare * 10000) + (are * 100) + centiare;
            string accessPath = @"https://localhost:5004/api/Places/" + idPlace + "/" + area + "/" + valAchat + "/" + nbrePge ;
            ResponseDTO responseDTO = new ResponseDTO();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(accessPath))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(apiResponse);   
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }
            }
            //Recuperation de l'objet Prefecture correspondant à la place dont l'id est fournie
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
                    {
                        ViewBag.StatusCode = response.StatusCode;
                    }
                }      
            }
            responseDTO.PrefectureName = prefecture.Name;
            responseDTO.Area = area;
            return View(responseDTO);    
        }        
    }
}
     