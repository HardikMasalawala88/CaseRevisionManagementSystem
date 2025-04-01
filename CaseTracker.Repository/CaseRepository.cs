using CaseTracker.Data.ContextModels;
using CaseTracker.Data.Enum;
using CaseTracker.Data.FormModels;
using CaseTracker.Data.ParameterModels;
using CaseTracker.Repository.Interface;
using CaseTracker.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CaseTracker.Repository
{
    public class CaseRepository : ICaseRepository
    {
        private readonly IRepository<Case> _caseRepository;
        private readonly IRepository<CaseDocument> _caseDocumentRepository;
        public CaseRepository(IRepository<Case> caseRepository, IRepository<CaseDocument> caseDocumentRepository)
        {
            _caseRepository = caseRepository;
            _caseDocumentRepository = caseDocumentRepository;
        }

        public void DeleteCase(long id)
        {
            Case caseInfo = GetCase(id);
            caseInfo.IsDelete = true;
            caseInfo.ModifiedDate = DateTime.UtcNow;
            caseInfo.ModifiedBy = caseInfo.CreatedBy;

            _caseRepository.SaveChanges();
        }

        public Case GetCase(long id)
        {
            return _caseRepository.GetById(id);
        }

        public IEnumerable<Case> GetCases()
        {
            return _caseRepository.GetAll();
        }

        public Case InsertCase(Case caseData)
        {
            Case caseDetail = _caseRepository.Insert(caseData);
            return caseDetail;
        }
        
        public void UpdateCase(Case caseData)
        {
            _caseRepository.Update(caseData);
        }

        public async Task<Paginate<Case>> GetCaseAsync(GetCaseParameters param)
        {
            var result = new Paginate<Case>();

            if (param == null)
            {
                return result;
            }

            var skip = param.PageSize * (param.Page - 1);

            var data = GetCases()
                      .Where(x => !x.IsDelete).AsQueryable();

            //if (!string.IsNullOrEmpty(param.SearchStr))
            //{
            //    param.SearchStr = param.SearchStr.Trim().ToLower();
            //    //data = data.Where(m => !string.IsNullOrEmpty(m.Client.Na) && m.User.Name.Trim().ToLower().Contains(param.SearchStr));
            //}

            if (param.StartDate.HasValue && param.EndDate.HasValue)
            {
                data = data.Where(m => m.HearingDate.Date >= param.StartDate.Value && m.HearingDate.Date <= param.EndDate.Value);
            }

            switch (param.SortLabel)
            {
                default:
                    // Handle any unexpected sort label here
                    data = data.OrderByDescending(x => x.Id);
                    break;
            }

            result.TotalCount = data.Count();
            result.Data = data.Skip(skip).Take(param.PageSize).Select(caseData => new Case
            {
                Id = caseData.Id,
                CaseTitle = caseData.CaseTitle,
                CaseDetail = caseData.CaseDetail,
                ClientId = caseData.ClientId,
                HearingDate = caseData.HearingDate,
                LawyerId = caseData.LawyerId,
                CourtLocation = caseData.CourtLocation,
                CaseNumber = caseData.CaseNumber,
                CaseParentId = caseData.CaseParentId,
                CreatedDate = caseData.CreatedDate,
            }).ToList();

            return result;
        }

        public bool BulkDeleteCase(List<long> ids)
        {
            var caseList = _caseRepository.GetAll().Where(x => ids.Contains(x.Id)).ToList();

            caseList.ForEach(caseData =>
            {
                caseData.IsDelete = true;
                _caseRepository.Update(caseData);
            });

            return true;
        }
    }
}
