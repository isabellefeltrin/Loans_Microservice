using LoansMicroservice.Model;
using Microsoft.EntityFrameworkCore;

namespace LoansMicroservice.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<LoansModel> Loans { get; set; }
    }
}

