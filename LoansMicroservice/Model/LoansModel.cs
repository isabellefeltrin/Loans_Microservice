namespace LoansMicroservice.Model
{
    public class LoansModel
    {
        public enum LoanStatus
        {
            Ativo = 0,
            Devolvido = 1
        }

        public int Id { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }

        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }

        public LoanStatus Status { get; set; } = LoanStatus.Ativo;
    }
}
