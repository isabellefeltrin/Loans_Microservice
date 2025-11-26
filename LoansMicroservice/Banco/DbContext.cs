using LoansMicroservice.Model;
using Microsoft.EntityFrameworkCore;

namespace LoansMicroservice.Banco
{
    public class LoansContext : DbContext
    {
        public LoansContext(DbContextOptions<LoansContext> options)
            : base(options)
        {
        }

        public DbSet<LoansModel> Loans { get; set; }
    }
}