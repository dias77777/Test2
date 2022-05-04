using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication11.Core;
using WebApplication11.Core.Models;
using WebApplication11.Persistence;

namespace WebApplication11.Controllers
{
    [ApiController]
    
    [Route("api/[controller]")]
    public class SalesPointController : ControllerBase
    {

        private readonly ILogger<SalesPointController> _logger;
        private readonly ISalesPointRepository _salesPointRepository;
        private readonly IUnitOfWork _unitOfWork;


        public SalesPointController(ISalesPointRepository salesPointRepository, IUnitOfWork unitOfWork, ILogger<SalesPointController> logger)
        {
            _salesPointRepository = salesPointRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;

        }

        [HttpGet]
        public async Task<IEnumerable<SalesPoint>> List()
        {

            return await _salesPointRepository.List();

        }

        [HttpPost]
        public async Task<IActionResult> Create(SalesPoint salesPoint)
        {
            await _salesPointRepository.Create(salesPoint);
            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new RespInfo(false, ex.Message));
            }


            return CreatedAtAction(nameof(GetById), new { id = salesPoint.Id }, salesPoint);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<SalesPoint>> GetById(int id)
        {

            var salesPoint = await _salesPointRepository.GetById(id);

            if (salesPoint == null)
            {
                return NotFound(new RespInfo(false, $"Item {id} not found."));


            }

            return Ok(salesPoint);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(SalesPoint salesPoint)
        {


            _salesPointRepository.Update(salesPoint);

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new RespInfo(false, ex.Message));
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            _salesPointRepository.Delete(id);

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new RespInfo(false, ex.Message));
            }


            return Ok();
        }





    }







}
