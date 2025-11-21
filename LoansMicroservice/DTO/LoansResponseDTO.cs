using static LoansMicroservice.Model.LoansModel;

namespace LoansMicroservice.DTO
{
    public class LoansResponseDTO
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }

        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }

        public LoanStatus Status { get; set; }
    }
}
