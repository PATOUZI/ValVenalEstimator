using CsvHelper;
using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.Json;
using System.Globalization;
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
    public class PrefecturesController : ControllerBase
    {
        private readonly IPrefectureRepository _iPrefectureRepository;
        private readonly IZoneRepository _iZoneRepository; 

        public PrefecturesController(IPrefectureRepository iPrefectureRepository, IZoneRepository iZoneRepository)
        {
            _iPrefectureRepository = iPrefectureRepository;
            _iZoneRepository = iZoneRepository;
        }   
   
        [HttpPost]
        public async Task<ActionResult> AddPrefecture(PrefectureDTO prefectureDTO)
        {
            try
            {
                return Ok( await _iPrefectureRepository.AddPrefectureAsync(prefectureDTO));  
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            } 
        }       

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPrefecture(long id)
        {
            try
            {
                return Ok(await _iPrefectureRepository.GetPrefectureAsync(id));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);            
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult> GetPrefectureByName(string name)
        {
            try
            {
                return Ok(await _iPrefectureRepository.GetPrefectureByNameAsync(name));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);            
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllPrefectures()
        {
            return Ok(await _iPrefectureRepository.GetAllPrefecturesAsync());
        }

        [HttpGet("WithZone")]
        public async Task<IActionResult> GetAllPrefecturesWithZones()
        {
            var list  =  await _iPrefectureRepository.GetAllPrefecturesAsync();
            var resList = list.Select( 
                                        async(p) => 
                                        {
                                            p.Zones = (await _iZoneRepository.GetAllZonesByPrefectureIdAsync(p.Id)).ToList();
                                            return p;
                                        }
                                     ).ToList();
            return Ok(resList);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrefecture(long id, PrefectureDTO prefectureDTO)
        {
            if (id != prefectureDTO.Id)
            {
                return BadRequest();
            }
            var p = await _iPrefectureRepository.GetPrefectureAsync(id); 
            if (p == null)
            {
                return NotFound();
            }
            p.Name = prefectureDTO.Name;
            try
            {
                _iPrefectureRepository.SaveChanges(); 
            }
            catch (DbUpdateConcurrencyException) when (!_iPrefectureRepository.PrefectureExists(id))
            {
                return NotFound();
            }
            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrefecture(long id)
        {
            var prefecture = await _iPrefectureRepository.GetPrefectureAsync(id);
            if (prefecture == null)
            {
                return NotFound();    
            }     
            _iPrefectureRepository.Remove(prefecture);           
            _iPrefectureRepository.SaveChanges();                                   
            return StatusCode(202);          
        }
   
        [HttpPost("LoadDataInDataBase")]    
        public void LoadDataInDb(string accessPath)
        {
            _iPrefectureRepository.LoadDataInDbWithCsvFile(accessPath);      
        }
    }     
}    