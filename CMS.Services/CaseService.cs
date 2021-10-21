using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using CMS.Repository.Interface;
using CMS.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services
{
    public class CaseService : ICaseService
    {
        private readonly ICaseRepository _clientAndcaseRepository;
        private readonly ApplicationContext _context;

        public CaseService(ICaseRepository clientAndcaseRepository, ApplicationContext context)
        {
            _clientAndcaseRepository = clientAndcaseRepository;
            _context = context;
        }
        public CaseFM CreateOrUpdateCase(CaseFM caseFM)
        {
            try
            {
                var caseDetail = _context.Cases.Where(x => x.Id == caseFM.Id).FirstOrDefault();
                if (caseDetail != null)
                {
                    Case caseInfo = new Case();
                    caseInfo.Id = caseDetail.Id;
                    caseInfo.ClientId = caseDetail.ClientId;
                    caseInfo.LawyerId = caseDetail.LawyerId;
                    caseInfo.HearingDate = caseDetail.HearingDate;
                    caseInfo.CaseDetail = caseDetail.CaseDetail;
                    caseInfo.CourtLocation = caseDetail.CourtLocation;
                    caseInfo.CaseParentId = caseDetail.CaseParentId;
                    caseInfo.ModifiedDate = DateTime.UtcNow;
                    caseInfo.ModifiedBy = caseDetail.CreatedBy;

                    _clientAndcaseRepository.UpdateCase(caseInfo);

                }
                else
                {
                    Case caseInfo = new Case();
                    caseInfo.ClientId = caseFM.Client.Id;
                    caseInfo.LawyerId = caseFM.Lawyer.Id;
                    caseInfo.HearingDate = caseFM.HearingDate;
                    caseInfo.CaseDetail = caseFM.CaseDetail;
                    caseInfo.CourtLocation = caseFM.CourtLocation;
                    caseInfo.CaseParentId = caseFM.CaseParentId;
                    caseInfo.CreatedBy = caseFM.Client.CreatedBy;

                    _clientAndcaseRepository.InsertCase(caseInfo);
                    caseFM.Id = caseInfo.Id;
                    caseFM.ClientId = caseInfo.ClientId;
                    caseFM.LawyerId = caseInfo.LawyerId;

                }
            }
            catch (Exception ex)
            {

            }
            return caseFM;
        }

        public IEnumerable<Case> ListCaseDetail()
        {
            var caseInfo = _clientAndcaseRepository.GetCases().Where(x => x.IsDelete != true).ToList();
            
            return caseInfo;
        }

        public bool RemoveCaseDetail(long caseId)
        {
            var caseData = _clientAndcaseRepository.GetCase(caseId);
            if (caseData != null)
            {
                _clientAndcaseRepository.DeleteCase(caseId);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
