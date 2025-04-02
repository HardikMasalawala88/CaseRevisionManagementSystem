using CaseTracker.Data.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTracker.Repository.Interface
{
    public interface ILawyerRepository
    {
        IEnumerable<Lawyer> GetLawyers();
        Lawyer GetLawyer(Guid id);
        Task<bool> IsExistAsync(Guid id);
        Lawyer InsertLawyer(Lawyer lawyer);
        void UpdateLawyer(Lawyer lawyer);
        void DeleteLawyer(Guid id);
    }
}
