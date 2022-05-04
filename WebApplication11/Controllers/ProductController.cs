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
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;


        public ProductController(IProductRepository productRepository, IUnitOfWork unitOfWork, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;

        }

        [HttpGet]
        public async Task<IEnumerable<Product>> List()
        {

            return await _productRepository.List();

        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await _productRepository.Create(product);
            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new RespInfo(false, ex.Message));
            }


            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {

            var product = await _productRepository.GetById(id);

            if (product == null)
            {
                return NotFound(new RespInfo(false, $"Item {id} not found."));


            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Product product)
        {


            _productRepository.Update(product);

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

            _productRepository.Delete(id);

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
