using Microsoft.EntityFrameworkCore;
using LoansMicroservice.Data;
using LoansMicroservice.Models;

namespace LoansMicroservice.Repositories
{
    public class LoansRepository
    {
        private readonly LoansDbContext _context;
        public LoansRepository(LoansDbContext context) => _context = context;

        public async Task<Loan> AddAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<Loan?> GetByIdAsync(int id) => await _context.Loans.FindAsync(id);
        public async Task<IEnumerable<Loan>> GetAllAsync() => await _context.Loans.ToListAsync();
        public async Task UpdateAsync(Loan loan)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<Member?> GetMemberByIdAsync(int id) => await _context.Members.FindAsync(id);
        public async Task UpdateMemberAsync(Member member)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync();
        }
    }
}
