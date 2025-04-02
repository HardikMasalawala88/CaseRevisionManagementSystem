using CaseTracker.Data.ContextModels;
using CaseTracker.Repository.Interface;
using CaseTracker.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseTracker.Repository
{
    public class LawyerRepository : ILawyerRepository
    {
        private readonly IRepository<Lawyer> _lawyerRepository;
        public LawyerRepository(IRepository<Lawyer> lawyerRepository)
        {
            _lawyerRepository = lawyerRepository;
        }
        public void DeleteLawyer(Guid id)
        {
            Lawyer lawyer = GetLawyer(id);
            lawyer.IsDelete = true;
            lawyer.ModifiedDate = DateTime.UtcNow;
            lawyer.ModifiedBy = lawyer.CreatedBy;

            //_lawyerRepository.Remove(lawyer);
            _lawyerRepository.SaveChanges();
        }

        public Task<bool> IsExistAsync(Guid id)
        {
            return _lawyerRepository.IsExistAsync(id);
        }

        public Lawyer GetLawyer(Guid id)
        {
            return _lawyerRepository.GetById(id);
        }

        public IEnumerable<Lawyer> GetLawyers()
        {
            return _lawyerRepository.GetAll();
        }

        public Lawyer InsertLawyer(Lawyer lawyer)
        {
            Lawyer lawyerdata = _lawyerRepository.Insert(lawyer);
            return lawyerdata;
        }

        public void UpdateLawyer(Lawyer lawyer)
        {
            _lawyerRepository.Update(lawyer);
        }
    }
}
