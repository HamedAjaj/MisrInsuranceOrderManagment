using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisrInsuranceOrderManagment.Domain.Entities;
using MisrInsuranceOrderManagment.Infrastructure;
using MisrInsuranceOrderManagment.Infrastructure.UnitOfwork;

namespace MisrInsuranceOrderManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfwork _unitOfWork;

        public CustomersController(IUnitOfwork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _unitOfWork.Repository<Customer>().GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            await _unitOfWork.Repository<Customer>().AddAsync(customer);
            await _unitOfWork.Save();
            return CreatedAtAction(nameof(GetById), new { id = customer.ID }, customer);
        }
    }
}
