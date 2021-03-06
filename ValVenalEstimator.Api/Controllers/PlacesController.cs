using AutoMapper;
using System;
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
        public async Task<ActionResult> AddPlace(PlaceDTO placeDTO) 
        {
            try
            {
                return Ok( await _iPlaceRepository.AddPlaceAsync(placeDTO));
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            } 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPlace(long id)
        {
            try
            {
                return Ok(await _iPlaceRepository.GetPlaceAsync(id));                
            }
            catch (Exception e)
            {              
                return NotFound(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlaces()
        {
            return Ok(await _iPlaceRepository.GetAllPlacesAsync());
        }

        [HttpGet ("zone/{idZone}")]
        public async Task<IEnumerable<Place>> GetPlacesByZoneId(long idZone)
        {
            return await _iPlaceRepository.GetPlacesByZoneIdAsync(idZone);
        }

        [HttpGet("prefecture/{idPrefecture}")]
        public async Task<IEnumerable<Place>> GetPlacesByPrefectureId(long idPrefecture)
        {
            return await _iPlaceRepository.GetPlacesByPrefectureIdAsync(idPrefecture);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlace(long id, PlaceDTO placeDTO)
        {
            if (id != placeDTO.Id)
            {
                return BadRequest();
            }
            var p = await _iPlaceRepository.GetPlaceAsync(id);
            if (p == null)
            {
                return NotFound();
            }
            p.Name = placeDTO.Name;
            p.ZoneId = placeDTO.ZoneId;
            try
            {
                _iPlaceRepository.SaveChanges(); 
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
            var place = await _iPlaceRepository.GetPlaceAsync(id);
            if (place == null)
            {
                return NotFound();    
            }
            _iPlaceRepository.Remove(place);            
            _iPlaceRepository.SaveChanges();                                   
            return StatusCode(202);
        }
    
        //Les 2 m??thodes suivantes font la meme chose. Elles sont diff??rentes par leur URI
        [HttpPost("{accessPath}", Name = "LoadDataInDb")]
        public void LoadDataInDb(string accessPath)
        {
            _iPlaceRepository.LoadDataInDbWithCsvFile(accessPath);
        }                        

        [HttpPost("LoadDataInDataBase")]      
        public void LoadDataInDb2(string accessPath)
        {
            _iPlaceRepository.LoadDataInDbWithCsvFile(accessPath);
        } 

        [HttpPost("LoadData")]      
        public void LoadData(string accessPath)
        {
            _iPlaceRepository.LoadData(accessPath);
        }                                    
        
        [HttpGet("{idPlace}/{area}/{valAchat}/{nbrePge}", Name = "GetValVenal")]
        public async Task<ActionResult<ResponseDTO>> GetPriceToPay(long idPlace, int area, double valAchat, int nbrePge)
        {
            try
            {
                return await _iPlaceRepository.GetPriceToPayAsync(idPlace, area, valAchat, nbrePge);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
