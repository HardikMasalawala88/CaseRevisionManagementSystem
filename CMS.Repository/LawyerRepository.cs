using CMS.Data.ContextModels;
using CMS.Repository.Interface;
using CMS.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repository
{
    public class LawyerRepository : ILawyerRepository
    {
        private readonly IRepository<Lawyer> _lawyerRepository;
        public LawyerRepository(IRepository<Lawyer> lawyerRepository)
        {
            _lawyerRepository = lawyerRepository;
        }
        public void DeleteLawyer(long id)
        {
            Lawyer lawyer = GetLawyer(id);
            _lawyerRepository.Remove(lawyer);
            _lawyerRepository.SaveChanges();
        }

        public Lawyer GetLawyer(long id)
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
