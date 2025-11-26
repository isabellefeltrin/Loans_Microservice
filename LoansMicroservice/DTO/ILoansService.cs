using LoansMicroservice.DTO;
using LoansMicroservice.Model;
using System.Collections.Generic;

namespace LoansMicroservice.Service
{
    public interface ILoansService
    {
        List<LoansResponseDTO> GetAll();
        LoansResponseDTO GetById(int id);
        void Create(LoansModel loan);
        void Update(LoansModel loan);
    }
}
