using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.Interface
{
    public interface ICaseService
    {
        CaseFM CreateOrUpdateCase(CaseFM caseFM);
        IEnumerable<Case> ListCaseDetail();
        bool RemoveCaseDetail(long caseId);
    }
}
