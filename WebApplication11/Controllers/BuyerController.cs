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
    public class BuyerController : ControllerBase
    {



        private readonly ILogger<BuyerController> _logger;

        private readonly IBuyerRepository _buyerRepository;
        private readonly IUnitOfWork _unitOfWork;


        public BuyerController(IBuyerRepository buyerRepository, IUnitOfWork unitOfWork, ILogger<BuyerController> logger)
        {
            _buyerRepository = buyerRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;

        }

        [HttpGet]
        public async Task<IEnumerable<Buyer>> List()
        {

            return await _buyerRepository.List();

        }

        [HttpPost]
        public async Task<IActionResult> Create(Buyer buyer)
        {
            await _buyerRepository.Create(buyer);
            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new RespInfo(false, ex.Message));
            }

            return CreatedAtAction(nameof(GetById), new { id = buyer.Id }, buyer);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Buyer>> GetById(int id)
        {

            var buyer = await _buyerRepository.GetById(id);
            if (buyer == null)
            {
                return NotFound(new RespInfo(false, $"Item {id} not found."));


            }

            return Ok(buyer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Buyer buyer)
        {


            _buyerRepository.Update(buyer);

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

            _buyerRepository.Delete(id);
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
