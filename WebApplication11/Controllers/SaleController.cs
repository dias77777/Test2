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
    public class SaleController : ControllerBase
    {
        private readonly ILogger<BuyerController> _logger;
        private readonly AppDbContext _context;
        private readonly ISaleService _saleService;
        private readonly ISaleRepository _saleRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SaleController(AppDbContext context, ISaleRepository saleRepository, ISaleService saleService, IUnitOfWork unitOfWork, ILogger<BuyerController> logger)
        {
            _context = context;
            _logger = logger;
            _saleService = saleService;
            _saleRepository = saleRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Sale>> List()
        {

            return await _saleRepository.List();

        }

        [HttpPost]
        public async Task<IActionResult> Create(Sale sale)
        {

            await _saleRepository.Create(sale);
            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new RespInfo(false, ex.Message));
            }


            return CreatedAtAction(nameof(GetById), new { id = sale.Id }, sale);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetById(int id)
        {

            var sale = await _saleRepository.GetById(id);

            if (sale == null)
            {
                return NotFound(new RespInfo(false, $"Item {id} not found."));
            }

            return Ok(sale);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Sale sale)
        {
            _saleRepository.Update(sale);

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
            _saleRepository.Delete(id);
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


        [HttpPost("Order")]
        public async Task<ActionResult<int>> Order(Order order)
        {
            try
            {
                var orderId = await _saleService.Order(order);

                return CreatedAtAction(nameof(GetById), new { id = orderId }, new RespInfo(true, $"Order successfully sent."));
            }
            catch (Exception ex)
            {
                return BadRequest(new RespInfo(false, ex.Message));
            }



        }



    }


}
