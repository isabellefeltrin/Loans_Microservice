using System.Collections.Generic;
using System.Reflection.Emit;
using static LoansMicroservice.Model.LoansModel;

namespace LoansMicroservice.Banco
{
    public class LoansContext : DbContext
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
            {
            }

            public DbSet<Loan> Loans { get; set; }
        }
    }
