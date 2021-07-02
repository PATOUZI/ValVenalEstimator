using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ValVenalEstimator.Api.Models;
using ValVenalEstimator.Api.Contracts;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        public async Task<ActionResult<Prefecture>> AddPrefecture(Prefecture prefecture)
        {
            await _iPrefectureRepository.AddPrefecture(prefecture);
            return CreatedAtAction(
                nameof(AddPrefecture),
                new { id = prefecture.Id },
                prefecture
            );
        }

        [HttpGet("{id}")]
        public async Task<Prefecture> GetPrefecture(long id)
        {
            return await _iPrefectureRepository.GetPrefecture(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPrefectures()
        {
            return Ok(await _iPrefectureRepository.GetAllPrefectures());
        }

        [HttpGet("WithZone")]
        public async Task<IActionResult> GetAllPrefecturesWithZones()
        {
            var list  =  await _iPrefectureRepository.GetAllPrefectures();
            var resList = list.Select( async(p) => {
                p.Zones = (await _iZoneRepository.GetAllZonesByPrefectureId(p.Id)).ToList();
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

            var p = await _iPrefectureRepository.GetPrefecture(id);
            if (p == null)
            {
                return NotFound();
            }

            p.Name = prefecture.Name;

            try
            {
                _iPrefectureRepository.SaveChange(); 
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
            var prefecture = await _iPrefectureRepository.GetPrefecture(id);

            if (prefecture == null)
            {
                return NotFound();    
            }     
            _iPrefectureRepository.Remove(prefecture);           
            _iPrefectureRepository.SaveChange();                                   
            return StatusCode(202);          
        }

        [HttpPost("{accessPath}")]
        public void LoadDataInDbByPost(string accessPath)
        {
            _iPrefectureRepository.LoadDataInDbWithCsvFile(accessPath);
        } 
    }
}