<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore;
using LoansMicroservice.Data;
using LoansMicroservice.Models;
=======
﻿using LoansMicroservice.Data;
using LoansMicroservice.Model;
using System.Collections.Generic;
using System.Linq;
>>>>>>> 54da52fad984003a64833e166d416e5bbcf56549

namespace LoansMicroservice.Repositories
{
    public class LoansRepository
    {
<<<<<<< HEAD
        private readonly LoansDbContext _context;
        public LoansRepository(LoansDbContext context) => _context = context;

        public async Task<Loan> AddAsync(Loan loan)
=======
        private readonly AppDbContext _context;

        public LoansRepository(AppDbContext context)
>>>>>>> 54da52fad984003a64833e166d416e5bbcf56549
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
