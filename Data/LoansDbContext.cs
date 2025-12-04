using Microsoft.EntityFrameworkCore;
using LoansMicroservice.Models;

namespace LoansMicroservice.Data
{
    public class LoansDbContext : DbContext
    {
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Member> Members { get; set; }

        public LoansDbContext(DbContextOptions<LoansDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>().HasKey(l => l.Id);
            modelBuilder.Entity<Member>().HasKey(m => m.Id);

            modelBuilder.Entity<Loan>().HasData(
                new Loan { Id = 1, MemberId = 1, BookId = 101, LoanDate = DateTime.UtcNow.AddDays(-5), Status = LoanStatus.Active }
            );

            modelBuilder.Entity<Member>().HasData(
                new Member { Id = 1, Name = "Alice", IsActive = true, CurrentLoans = 0, LoanLimit = 3 },
                new Member { Id = 2, Name = "Bob", IsActive = true, CurrentLoans = 1, LoanLimit = 2 }
            );
        }
    }
}
