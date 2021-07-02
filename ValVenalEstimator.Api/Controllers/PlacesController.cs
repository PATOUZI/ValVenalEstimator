using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValVenalEstimator.Api.Models;
using ValVenalEstimator.Api.Contracts;
using ValVenalEstimator.Api.ViewModels;
namespace ValVenalEstimator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly IPlaceRepository _iPlaceRepository;
        public PlacesController(IPlaceRepository iPlaceRepository)
        {
            _iPlaceRepository = iPlaceRepository;
        }

        [HttpPost]
        public async Task<Place> AddPlace(Place place)
        {
            await _iPlaceRepository.AddAsyncPlace(place);
            /*return CreatedAtAction(
                nameof(AddPlace),
                new { id = place.Id },
                place
            );*/
            return place;
        }

        [HttpGet("{id}")]
        public async Task<Place> GetPlace(long id)
        {
            return await _iPlaceRepository.GetAsyncPlace(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Place>> GetAllPlaces()
        {
            return await _iPlaceRepository.GetAsyncAllPlaces();
        }

        [HttpGet ("zone/{idZone}")]
        public async Task<IEnumerable<Place>> GetPlacesByZoneId(long idZone)
        {
            return await _iPlaceRepository.GetAsyncPlacesByZoneId(idZone);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlace(long id, Place place)
        {
            if (id != place.Id)
            {
                return BadRequest();
            }
            var p = await _iPlaceRepository.GetAsyncPlace(id);
            if (p == null)
            {
                return NotFound();
            }
            p.Name = place.Name;
            p.ZoneId = place.ZoneId;
            try
            {
                _iPlaceRepository.SaveAsyncChange(); 
            }
            catch (DbUpdateConcurrencyException) when (!_iPlaceRepository.PlaceExists(id))
            {
                return NotFound();
            }
            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlace(long id)
        {
            var place = await _iPlaceRepository.GetAsyncPlace(id);
            if (place == null)
            {
                return NotFound();    
            }
            _iPlaceRepository.Remove(place);            
            _iPlaceRepository.SaveAsyncChange();                                   
            return StatusCode(202);
        }
    
        [HttpPost("{accessPath}", Name = "LoadDataInDbByPost")]
        public void LoadDataInDbByPost(string accessPath)
        {
            _iPlaceRepository.LoadAsyncDataInDbWithCsvFile(accessPath);
        }                     

        [HttpPost("LoadDataInDataBase")]      
        public void Load(string accessPath)
        {
            _iPlaceRepository.LoadAsyncDataInDbWithCsvFile(accessPath);
        }                                     

        /*[HttpGet("Load/{accessPath}")]
        public void LoadDatas(string accessPath)
        {
            _iPlaceRepository.LoadDataInDbWithCsvFile(accessPath);
        }*/           

        /*[HttpGet("LoadDataInDb")]
        public IActionResult LoadDataInDb(string accessPath)
        {
            _iPlaceRepository.LoadDataInDbWithCsvFile(accessPath);
            return Ok(GetAllPlaces());
        }*/    

        [HttpGet("{id}/{area}", Name = "GetValVenal")]
        public async Task<ActionResult<ValVenalDTO>> GetValVenal(long id, int area)
        {
            var place = await _iPlaceRepository.GetAsyncPlace(id);
            if (place == null)
            {
                return NotFound();
            }
            ValVenalDTO venaleValue = new ValVenalDTO();
            venaleValue.ValVenal = place.Zone.PricePerMeterSquare * area;
            return venaleValue;
        }
    }
}
