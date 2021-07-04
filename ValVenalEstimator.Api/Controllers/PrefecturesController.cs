using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValVenalEstimator.Api.Models;
using ValVenalEstimator.Api.Contracts;

namespace ValVenalEstimator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrefecturesController : ControllerBase
    {
        private readonly IPrefectureRepository _iPrefectureRepository;
        private readonly IZoneRepository _iZoneRepository; 
        public PrefecturesController(IPrefectureRepository iPrefectureRepository,IZoneRepository iZoneRepository)
        {
            _iPrefectureRepository = iPrefectureRepository;
            _iZoneRepository = iZoneRepository;
        }   

        [HttpPost]
        public async Task<ActionResult> AddPrefecture(Prefecture prefecture)
        {
            try
            {
                return Ok( await _iPrefectureRepository.AddPrefectureAsync(prefecture));
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }   
        }

        [HttpGet("{id}")]
        public async Task<Prefecture> GetPrefecture(long id)
        {
            return await _iPrefectureRepository.GetPrefectureAsync(id);
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
            var resList = list.Select( async(p) => {
                p.Zones = (await _iZoneRepository.GetAllZonesByPrefectureIdAsync(p.Id)).ToList();
                return p;
            }).ToList();
            //List<Prefecture> resList = new List<Prefecture>(); 
            // foreach (var p in list)
            // {
            //     p.Zones = (await _iZoneRepository.GetAllZonesByPrefectureId(p.Id)).ToList();
                
            // }
            return Ok(resList);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrefecture(long id, Prefecture prefecture)
        {
            if (id != prefecture.Id)
            {
                return BadRequest();
            }
            var p = await _iPrefectureRepository.GetPrefectureAsync(id); 
            if (p == null)
            {
                return NotFound();
            }
            p.Name = prefecture.Name;
            try
            {
                _iPrefectureRepository.SaveChangeAsync(); 
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
            _iPrefectureRepository.SaveChangeAsync();                                   
            return StatusCode(202);          
        }

        [HttpPost("{accessPath}")]
        public void LoadDataInDbByPost(string accessPath)
        {
            _iPrefectureRepository.LoadDataInDbWithCsvFileAsync(accessPath);
        } 
    }
}