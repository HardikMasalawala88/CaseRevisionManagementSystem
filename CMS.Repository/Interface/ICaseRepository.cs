using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using CMS.Data.ParameterModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Repository.Interface
{
    public interface ICaseRepository
    {
        IEnumerable<Case> GetCases();
        Case GetCase(long id);
        bool BulkDeleteCase(List<long> ids);
        Task<Paginate<Case>> GetCaseAsync(GetCaseParameters param);
        Case InsertCase(Case caseData);
        void UpdateCase(Case caseData);
        void DeleteCase(long id);
    }
}
