using CMS.Data.ContextModels;
using CMS.Repository.Interface;
using CMS.Repository.Repository;
using System;
using System.Collections.Generic;

namespace CMS.Repository
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
    }
}
