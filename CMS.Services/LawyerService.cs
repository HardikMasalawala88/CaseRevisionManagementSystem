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
    public class LawyerService : ILawyerService
    {
        private readonly ILawyerRepository _lawyerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationContext _context;

        public LawyerService(ILawyerRepository lawyerRepository, ApplicationContext context, IUserRepository userRepository)
        {
            _lawyerRepository = lawyerRepository;
            _userRepository = userRepository;
            _context = context;
        }

        public LawyerFM CreateOrUpdateLawyer(LawyerFM lawyerFM)
        {
            try
            {
                var lawyerDetail = _context.Lawyers.Where(x => x.Id == lawyerFM.Id).FirstOrDefault();
                //var userDetail = _context.UserData.Where(x => x.Id == userId).FirstOrDefault();
                if (lawyerDetail != null)
                {
                    User user = new User();
                    user.Name = lawyerFM.User.Name;
                    user.EmailId = lawyerFM.User.EmailId;
                    user.MobileNo = lawyerFM.User.MobileNo;
                    user.Address = lawyerFM.User.Address;
                    user.City = lawyerFM.User.City;
                    user.Gender = lawyerFM.User.Gender;
                    user.ModifiedBy = lawyerFM.User.Name;
                    user.Role = lawyerFM.User.Role;
                    user.Username = lawyerFM.User.Username;
                    user.Password = lawyerFM.User.Password;
                    user.ModifiedDate = DateTime.UtcNow;
                    _userRepository.UpdateUser(user);

                    Lawyer lawyer = new Lawyer();
                    lawyer.Id = lawyerDetail.Id;
                    lawyer.UserId = user.Id;
                    lawyer.DateOfBirth = lawyerDetail.DateOfBirth;
                    lawyer.AadharNumber = lawyerDetail.AadharNumber;
                    lawyer.PanCardNumber = lawyerDetail.PanCardNumber;
                    lawyer.Lawyer_uniqueNumber = lawyerDetail.Lawyer_uniqueNumber;
                    lawyer.VotingId = lawyerDetail.VotingId;
                    lawyer.SpecializationId = lawyerDetail.SpecializationId;
                    lawyer.CaseId = lawyerDetail.CaseId;
                    lawyer.AppointmentId = lawyerDetail.AppointmentId;
                    lawyer.ModifiedDate = DateTime.UtcNow;
                    lawyer.ModifiedBy = user.Name;

                    _lawyerRepository.UpdateLawyer(lawyer);
                }
                else
                {
                    User user = new User();
                    user.Name = lawyerFM.User.Name;
                    user.EmailId = lawyerFM.User.EmailId;
                    user.MobileNo = lawyerFM.User.MobileNo;
                    user.Address = lawyerFM.User.Address;
                    user.City = lawyerFM.User.City;
                    user.Gender = lawyerFM.User.Gender;
                    user.ModifiedBy = lawyerFM.User.Name;
                    user.Role = lawyerFM.User.Role;
                    user.Username = lawyerFM.User.Username;
                    user.Password = lawyerFM.User.Password;
                    user.ModifiedDate = DateTime.UtcNow;
                    _userRepository.InsertUser(user);

                    Lawyer lawyer = new Lawyer();
                    lawyer.UserId = user.Id;
                    lawyer.DateOfBirth = lawyerFM.DateOfBirth;
                    lawyer.AadharNumber = lawyerFM.AadharNumber;
                    lawyer.PanCardNumber = lawyerFM.PanCardNumber;
                    lawyer.Lawyer_uniqueNumber = lawyerFM.Lawyer_uniqueNumber;
                    lawyer.VotingId = lawyerFM.VotingId;
                    lawyer.SpecializationId = lawyerFM.SpecializationId;
                    lawyer.CaseId = lawyerFM.CaseId;
                    lawyer.AppointmentId = lawyerFM.AppointmentId;
                    lawyer.CreatedBy = user.Name;
                    
                    _lawyerRepository.InsertLawyer(lawyer);
                    lawyerFM.Id = lawyer.Id;
                }
            }
            catch (Exception ex)
            {

            }
            return lawyerFM;
        }
    }
}
