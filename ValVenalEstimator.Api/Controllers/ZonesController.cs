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
        public ZonesController(IZoneRepository iZoneRepository)
        {
            _iZoneRepository = iZoneRepository;
        }   

        [HttpPost]
        public async Task<ActionResult> AddZone(ZoneDTO zoneDTO)
        {
            try
            {
                return Ok(await _iZoneRepository.AddZoneAsync(zoneDTO));
            }
            catch(Exception e)
            {
                 return NotFound(e.Message);
            }        
        }

        [HttpGet("{id}")]
        public async Task<Zone> GetZone(long id)
        {
            return await _iZoneRepository.GetZoneAsync(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllZones()
        {
            return Ok(await _iZoneRepository.GetAllZonesAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateZone(long id, Zone zone)
        {
            if (id != zone.Id)
            {
                return BadRequest();
            }
            var z = await _iZoneRepository.GetZoneAsync(id);
            if (z == null)
            {
                return NotFound();
            }
            z.Name = zone.Name;
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