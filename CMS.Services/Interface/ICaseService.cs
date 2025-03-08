using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using CMS.Data.ParameterModels;
using CMS.Data.ServiceResponse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Services.Interface
{
    public interface ICaseService
    {
        CaseFM CreateOrUpdateCase(CaseFM caseFM);
        CaseDocumentFM CreateOrUpdateCaseDocument(CaseDocumentFM caseDocument);
        List<CaseDocument> ListCaseDocDetail();
        List<Case> ListCaseDetail();
        List<Case> ListClientCases(long clientId);
        bool RemoveCaseDetail(long caseId);
        CaseFM GetCaseById(long caseId);
        Task<ServiceResponse<Paginate<Case>>> GetCaseAsync(GetCaseParameters getCaseParameters);
        ServiceResponse<bool> BulkDeleteCase(List<long> ids);
    }
}
