using CMS.Data.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repository.Interface
{
    public interface ILawyerRepository
    {
        IEnumerable<Lawyer> GetLawyers();
        Lawyer GetLawyer(long id);
        Lawyer InsertLawyer(Lawyer lawyer);
        void UpdateLawyer(Lawyer lawyer);
        void DeleteLawyer(long id);
    }
}
