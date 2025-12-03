using LoansMicroservice.Data;
using LoansMicroservice.DTO;
using LoansMicroservice.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LoansMicroservice.Service
{
    public class LoansService : ILoansService
    {
        private readonly AppDbContext _context;

        public LoansService(AppDbContext context)
        {
            _context = context;
        }

        public List<LoansResponseDTO> GetAll()
        {
            return _context.Loans
                .Select(l => new LoansResponseDTO
                {
                    Id = l.Id,
                    BookId = l.BookId,
                    MemberId = l.MemberId,
                    DataEmprestimo = l.DataEmprestimo,
                    DataDevolucao = l.DataDevolucao,
                    Status = l.Status.ToString()    
                }).ToList();
        }

        public LoansResponseDTO GetById(int id)
        {
            var l = _context.Loans.FirstOrDefault(l => l.Id == id);
            if (l == null) return null;

            return new LoansResponseDTO
            {
                Id = l.Id,
                BookId = l.BookId,
                MemberId = l.MemberId,
                DataEmprestimo = l.DataEmprestimo,
                DataDevolucao = l.DataDevolucao,
                Status = l.Status.ToString()
            };
        }

        public void Create(LoansModel loan)
        {
            _context.Loans.Add(loan);
            _context.SaveChanges();
        }

        public void Update(LoansModel loan)
        {
            _context.Loans.Update(loan);
            _context.SaveChanges();
        }
    }
}
