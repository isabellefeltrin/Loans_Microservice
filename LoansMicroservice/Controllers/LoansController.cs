using Microsoft.AspNetCore.Mvc;
using LoansMicroservice.Models;
using LoansMicroservice.Services;

namespace LoansMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly LoansService _service;
        public LoansController(LoansService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Create(CreateLoanDto dto)
        {
            try
            {
                var loan = await _service.CreateLoan(dto);
                return CreatedAtAction(nameof(GetById), new { id = loan.Id }, loan);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllLoans());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var loan = await _service.GetLoanById(id);
            if (loan == null) return NotFound();
            return Ok(loan);
        }

        [HttpPatch("{id}/return")]
        public async Task<IActionResult> Return(int id)
        {
            try
            {
                var loan = await _service.ReturnLoan(id);
                return Ok(new { message = "Devolução registrada com sucesso.", loan });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
