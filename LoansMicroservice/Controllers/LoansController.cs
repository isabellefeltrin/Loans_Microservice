using LoansMicroservice.DTO;
using LoansMicroservice.Model;
using LoansMicroservice.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LoansMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoansService _service;

        public LoansController(ILoansService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LoansResponseDTO>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<LoansResponseDTO> GetById(int id)
        {
            var loan = _service.GetById(id);
            if (loan == null) return NotFound();
            return Ok(loan);
        }

        [HttpPost]
        public IActionResult Create(LoansModel loan)
        {
            _service.Create(loan);
            return CreatedAtAction(nameof(GetById), new { id = loan.Id }, loan);
        }

        [HttpPut]
        public IActionResult Update(LoansModel loan)
        {
            _service.Update(loan);
            return NoContent();
        }
    }
}
