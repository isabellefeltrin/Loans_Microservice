namespace LoansMicroservice.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public int CurrentLoans { get; set; } = 0;
        public int LoanLimit { get; set; } = 3;
    }
}
