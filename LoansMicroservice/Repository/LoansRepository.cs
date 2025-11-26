using LoansMicroservice.Model;
using LoansMicroservice.Banco;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LoansMicroservice.Repository
{
    public class LoansRepository
    {
        private readonly LoansDbContext _context;

        public LoansRepository(LoansDbContext context)
        {
            _context = context;
        }

        public List<LoansModel> GetAll()
        {
            return _context.Loans.ToList();
        }

        public LoansModel GetById(int id)
        {
            return _context.Loans.FirstOrDefault(l => l.Id == id);
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
