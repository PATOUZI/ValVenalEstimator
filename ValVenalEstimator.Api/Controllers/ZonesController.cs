using AutoMapper;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValVenalEstimator.Api.Models;
using ValVenalEstimator.Api.ViewModels;
using ValVenalEstimator.Api.Contracts;

namespace ValVenalEstimator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZonesController : ControllerBase
    {
        private readonly IZoneRepository _iZoneRepository;  
        private readonly IMapper _mapper;

        public ZonesController(IZoneRepository iZoneRepository, IMapper mapper)
        {
            _iZoneRepository = iZoneRepository;
            _mapper = mapper;
        }   

        [HttpPost]
        public async Task<ActionResult> AddZone(ZoneDTO zoneDTO)
        {
            try
            {
                return Ok(await _iZoneRepository.AddZoneAsync(zoneDTO));       
            }
            catch (Exception e)
            {
                
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetZone(long id)
        {
            try
            {
                return Ok(await _iZoneRepository.GetZoneAsync(id));
           
            }
            catch (Exception e)
            {
                return NotFound(e.Message);                            
            }
        }

        /*[HttpGet]
        public async Task<IActionResult> GetAllZones()
        {
            try
            {
                return Ok(await _iZoneRepository.GetAllZonesAsync()); 
            }
            catch (Exception e)
            {    
                return NotFound(e.Message);                            
            }
        }*/   

         [HttpGet]
        //public async Task<IEnumerable<ZoneViewDTO>> GetAllZones()
        public async Task<IActionResult> GetAllZones()
        {
            var zones = await _iZoneRepository.GetAllZonesAsync(); 
            var resources = _mapper.Map<IEnumerable<Zone>, IEnumerable<ZoneViewDTO>>(zones);
            return Ok(resources);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateZone(long id, ZoneDTO zoneDTO)
        {
            if (id != zoneDTO.Id)
            {
                return BadRequest();
            }
            var z = await _iZoneRepository.GetZoneAsync(id);
            if (z == null)
            {
                return NotFound();
            }
            z.Name = zoneDTO.Name;
            z.ZoneNum = zoneDTO.ZoneNum;
            z.Type = zoneDTO.Type;
            z.PricePerMeterSquare = zoneDTO.PricePerMeterSquare;
            z.PrefectureId = zoneDTO.PrefectureId;
            try
            {
                _iZoneRepository.SaveChangeAsync(); 
            }
            catch (DbUpdateConcurrencyException) when (!_iZoneRepository.ZoneExists(id))
            {
                return NotFound();
            }

            return StatusCode(200);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZone(long id)
        {
            var zone = await _iZoneRepository.GetZoneAsync(id);
            if (zone == null)
            {
                return NotFound();    
            }     
            _iZoneRepository.Remove(zone);           
            _iZoneRepository.SaveChangeAsync();                                   
            return StatusCode(202);          
        }

        [HttpPost("{accessPath}")]
        public void LoadDataInDbByPost(string accessPath)
        {
            _iZoneRepository.LoadDataInDbWithCsvFileAsync(accessPath);
        } 
    }
}