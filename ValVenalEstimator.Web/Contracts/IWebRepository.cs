
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ValVenalEstimator.Web.Models;

namespace ValVenalEstimator.Web.Contracts
{
    public interface IWebRepository
    {
        public Task<List<Prefecture>> Index();
        public Task<bool> PlaceUploadFile(IFormFile file);
        public Task<bool> PrefectureUploadFile(IFormFile file);
        public Task<bool> ZoneUploadFile(IFormFile file);
        public Task<bool> UploadFile(IFormFile file, string directory, string accessPath);

         
    }
}