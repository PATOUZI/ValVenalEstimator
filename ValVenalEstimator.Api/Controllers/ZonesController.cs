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
        private readonly IPrefectureRepository _iPrefectureRepository;  
        private readonly IMapper _mapper;

        public ZonesController(IZoneRepository iZoneRepository, IMapper mapper, IPrefectureRepository iPrefectureRepository)
        {
            _iZoneRepository = iZoneRepository;
            _iPrefectureRepository = iPrefectureRepository;
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
                var zone = await _iZoneRepository.GetZoneAsync(id); 
                var resource = _mapper.Map<Zone, ZoneViewDTO>(zone);
                return Ok(resource);   
            }
            catch (Exception e)
            {
                return NotFound(e.Message);                                            
            }
        }

        [HttpGet("{zoneName}/{prefectName}")]
        public async Task<ActionResult> GetZoneByZoneNameAndPrefectureName(string zoneName, string prefectName)
        {
            try
            {
                var zone = await _iZoneRepository.GetZoneByZoneNameAndPrefectureNameAsync(zoneName, prefectName); 
                var resource = _mapper.Map<Zone, ZoneViewDTO>(zone);
                return Ok(resource); 
                //return Ok(zone); 
            }
            catch (Exception e)
            {
                return NotFound(e.Message);                                            
            }
        } 

        [HttpGet]
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
            var prefecture = await _iPrefectureRepository.GetPrefectureAsync(zoneDTO.PrefectureId);
            if (prefecture != null)    
            {
                z.PrefectureId = zoneDTO.PrefectureId; 
                z.Prefecture = prefecture;
                z.Code = prefecture.Name + "_" + z.Name;
            }
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

        [HttpPost("LoadDataInDataBase")]
        public void LoadDataInDbByPost(string accessPath)
        {
            _iZoneRepository.LoadDataInDbWithCsvFileAsync(accessPath);
        } 

        [HttpPost("LoadData")]   
        public void LoadData(string accessPath)
        {
            _iZoneRepository.LoadData(accessPath);
        } 
    }
}