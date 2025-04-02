using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;
using CaseTracker.Data.ParameterModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseTracker.Repository.Interface
{
    public interface ICaseRepository
    {
        IEnumerable<Case> GetCases();
        Case GetCase(Guid id);
        bool BulkDeleteCase(List<Guid> ids);
        Task<Paginate<Case>> GetCaseAsync(GetCaseParameters param);
        Case InsertCase(Case caseData);
        void UpdateCase(Case caseData);
        void DeleteCase(Guid id);
    }
}
