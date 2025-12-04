using LoansMicroservice.Models;
using LoansMicroservice.Repositories;

namespace LoansMicroservice.Services
{
    public class LoansService
    {
        private readonly LoansRepository _repository;
        private readonly ExternalServicesHelper _externalServices;

        public LoansService(LoansRepository repository, ExternalServicesHelper externalServices)
        {
            _repository = repository;
            _externalServices = externalServices;
        }

        public async Task<Loan> CreateLoan(CreateLoanDto dto)
        {
            var member = await _repository.GetMemberByIdAsync(dto.MemberId);
            if (member == null) throw new Exception("Membro não encontrado.");
            if (!member.IsActive) throw new Exception("Membro inativo.");
            if (member.CurrentLoans >= member.LoanLimit) throw new Exception("Limite de empréstimos atingido.");

            var book = await _externalServices.CheckBookAvailability(dto.BookId);
            if (book == null) throw new Exception("Livro não encontrado.");
            if (book.AvailableCopies <= 0) throw new Exception("Não há cópias disponíveis.");

            var loan = new Loan { MemberId = dto.MemberId, BookId = dto.BookId };
            var createdLoan = await _repository.AddAsync(loan);

            member.CurrentLoans++;
            await _repository.UpdateMemberAsync(member);

            var bookUpdateSuccess = await _externalServices.UpdateBookQuantity(dto.BookId, book.AvailableCopies - 1);
            if (!bookUpdateSuccess) Console.WriteLine("Falha ao atualizar quantidade do livro.");

            return createdLoan;
        }

        public async Task<Loan> ReturnLoan(int loanId)
        {
            var loan = await _repository.GetByIdAsync(loanId);
            if (loan == null) throw new Exception("Empréstimo não encontrado.");
            if (loan.Status == LoanStatus.Returned) throw new Exception("Empréstimo já devolvido.");

            loan.Status = LoanStatus.Returned;
            loan.ReturnDate = DateTime.UtcNow;
            await _repository.UpdateAsync(loan);

            var member = await _repository.GetMemberByIdAsync(loan.MemberId);
            if (member != null)
            {
                member.CurrentLoans = Math.Max(0, member.CurrentLoans - 1);
                await _repository.UpdateMemberAsync(member);
            }

            var book = await _externalServices.CheckBookAvailability(loan.BookId);
            if (book != null)
            {
                var success = await _externalServices.UpdateBookQuantity(loan.BookId, book.AvailableCopies + 1);
                if (!success) Console.WriteLine("Falha ao atualizar quantidade do livro (devolução).");
            }

            return loan;
        }

        public async Task<IEnumerable<Loan>> GetAllLoans() => await _repository.GetAllAsync();
        public async Task<Loan?> GetLoanById(int id) => await _repository.GetByIdAsync(id);
    }
}
