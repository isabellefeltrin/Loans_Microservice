<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;
using LoansMicroservice.Models;
using LoansMicroservice.Services;
=======
﻿using LoansMicroservice.DTO;
using LoansMicroservice.Model;
using LoansMicroservice.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
>>>>>>> 54da52fad984003a64833e166d416e5bbcf56549

namespace LoansMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
<<<<<<< HEAD
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
=======
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
>>>>>>> 54da52fad984003a64833e166d416e5bbcf56549
        }
    }
}
