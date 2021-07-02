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
        public async Task<ActionResult<Prefecture>> AddPrefecture(Prefecture prefecture)
        {
            await _iPrefectureRepository.AddAsyncPrefecture(prefecture);
            return CreatedAtAction(
                nameof(AddPrefecture),
                new { id = prefecture.Id },
                prefecture
            );
        }

        [HttpGet("{id}")]
        public async Task<Prefecture> GetPrefecture(long id)
        {
            return await _iPrefectureRepository.GetAsyncPrefecture(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPrefectures()
        {
            return Ok(await _iPrefectureRepository.GetAsyncAllPrefectures());
        }

        [HttpGet("WithZone")]
        public async Task<IActionResult> GetAllPrefecturesWithZones()
        {
            var list  =  await _iPrefectureRepository.GetAsyncAllPrefectures();
            var resList = list.Select( async(p) => {
                p.Zones = (await _iZoneRepository.GetAsyncAllZonesByPrefectureId(p.Id)).ToList();
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

            var p = await _iPrefectureRepository.GetAsyncPrefecture(id);
            if (p == null)
            {
                return NotFound();
            }

            p.Name = prefecture.Name;

            try
            {
                _iPrefectureRepository.SaveAsyncChange(); 
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
            var prefecture = await _iPrefectureRepository.GetAsyncPrefecture(id);

            if (prefecture == null)
            {
                return NotFound();    
            }     
            _iPrefectureRepository.Remove(prefecture);           
            _iPrefectureRepository.SaveAsyncChange();                                   
            return StatusCode(202);          
        }

        [HttpPost("{accessPath}")]
        public void LoadDataInDbByPost(string accessPath)
        {
            _iPrefectureRepository.LoadAsyncDataInDbWithCsvFile(accessPath);
        } 
    }
}