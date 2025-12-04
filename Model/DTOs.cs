namespace LoansMicroservice.Models
{
    public record CreateLoanDto(int MemberId, int BookId);
    public record BookCheckDto(int Id, string Title, int AvailableCopies);
    public record MemberCheckDto(int Id, bool IsActive, int CurrentLoans, int LoanLimit);
}
