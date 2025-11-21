using LoansMicroservice.Banco;
using LoansMicroservice.DTO;
using LoansMicroservice.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using static LoansMicroservice.Banco.LoansContext;
using static LoansMicroservice.Model.LoansModel;

namespace LoansMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;


        private const string BooksBaseUrl = "http://localhost:5001";
        private const string MembersBaseUrl = "http://localhost:5002";

        public LoansController(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet]
        public ActionResult<IEnumerable<LoansResponseDTO>> GetAll()
        {
            var loans = _context.Loans.ToList();

            var result = loans.Select(l => new LoansResponseDTO
            {
                Id = l.Id,
                BookId = l.BookId,
                MemberId = l.MemberId,
                DataEmprestimo = l.DataEmprestimo,
                DataDevolucao = l.DataDevolucao,
                Status = l.Status.ToString()
            });

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<LoansResponseDTO> GetById(int id)
        {
            var loan = _context.Loans.Find(id);
            if (loan == null) return NotFound();

            var result = new LoansResponseDTO
            {
                Id = loan.Id,
                BookId = loan.BookId,
                MemberId = loan.MemberId,
                DataEmprestimo = loan.DataEmprestimo,
                DataDevolucao = loan.DataDevolucao,
                Status = loan.Status.ToString()
            };

            return Ok(result);
        }


        [HttpPost]
        public ActionResult<LoansResponseDTO> Create([FromBody] CreateLoanRequest request)
        {
            var client = _httpClientFactory.CreateClient();


            var bookResponse = client.GetAsync($"{BooksBaseUrl}/api/books/{request.BookId}")
                                     .GetAwaiter()
                                     .GetResult();

            if (!bookResponse.IsSuccessStatusCode)
                return BadRequest("Livro não encontrado.");

            var book = bookResponse.Content.ReadFromJsonAsync<bookDTOResponse>()
                                          .GetAwaiter()
                                          .GetResult();

            if (book == null)
                return BadRequest("Erro ao consultar livro.");

            if (book.Quantity <= 0)
                return BadRequest("Não há cópias disponíveis desse livro.");

            // 2) Consulta membro (members-service)
            var memberResponse = client.GetAsync($"{MembersBaseUrl}/api/members/{request.MemberId}")
                                       .GetAwaiter()
                                       .GetResult();

            if (!memberResponse.IsSuccessStatusCode)
                return BadRequest("Membro não encontrado.");

            var member = memberResponse.Content.ReadFromJsonAsync<MemberDTOResponse>()
                                              .GetAwaiter()
                                              .GetResult();

            if (member == null)
                return BadRequest("Erro ao consultar membro.");


            var loan = new Loan
            {
                BookId = request.BookId,
                MemberId = request.MemberId,
                DataEmprestimo = DateTime.UtcNow,
                Status = LoanStatus.Ativo
            };

            _context.Loans.Add(loan);
            _context.SaveChanges();

            var patchBook = client.PatchAsync(
                    $"{BooksBaseUrl}/api/books/{request.BookId}/decrement-copies", content: null)
                .GetAwaiter()
                .GetResult();


            var patchMember = client.PatchAsync(
                    $"{MembersBaseUrl}/api/members/{request.MemberId}/increment-active-loans", content: null)
                .GetAwaiter()
                .GetResult();

            if (!patchBook.IsSuccessStatusCode || !patchMember.IsSuccessStatusCode)
            {
                return StatusCode(500, "Empréstimo criado, mas houve falha ao atualizar livros ou membros.");
            }

            var response = new LoansResponseDTO
            {
                Id = loan.Id,
                BookId = loan.BookId,
                MemberId = loan.MemberId,
                DataEmprestimo = loan.DataEmprestimo,
                DataDevolucao = loan.DataDevolucao,
                Status = loan.Status.ToString()
            };

            return CreatedAtAction(nameof(GetById), new { id = loan.Id }, response);
        }

        // PATCH api/loans/{id}/return
        [HttpPatch("{id:int}/return")]
        public IActionResult Return(int id)
        {
            var loan = _context.Loans.Find(id);
            if (loan == null) return NotFound();

            if (loan.Status == LoanStatus.Devolvido)
                return BadRequest("Empréstimo já foi devolvido.");

            loan.Status = LoanStatus.Devolvido;
            loan.DataDevolucao = DateTime.UtcNow;

            _context.SaveChanges();

            var client = _httpClientFactory.CreateClient();

            // Atualizar livros e membros na devolução
            var patchBook = client.PatchAsync(
                    $"{BooksBaseUrl}/api/books/{loan.BookId}/increment-copies", content: null)
                .GetAwaiter()
                .GetResult();

            var patchMember = client.PatchAsync(
                    $"{MembersBaseUrl}/api/members/{loan.MemberId}/decrement-active-loans", content: null)
                .GetAwaiter()
                .GetResult();

            if (!patchBook.IsSuccessStatusCode || !patchMember.IsSuccessStatusCode)
            {
                return StatusCode(500, "Devolução registrada, mas houve falha ao atualizar livros ou membros.");
            }

            return NoContent();
        }
    }
}
