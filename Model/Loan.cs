namespace LoansMicroservice.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateTime LoanDate { get; set; } = DateTime.UtcNow;
        public DateTime? ReturnDate { get; set; }
        public LoanStatus Status { get; set; } = LoanStatus.Active;
    }

    public enum LoanStatus
    {
        Active = 1,
        Returned = 2
    }
}
