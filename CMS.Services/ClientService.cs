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
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly ApplicationContext _context;

        public ClientService(IClientRepository clientRepository, ApplicationContext context, IUserRepository userRepository)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _context = context;
        }
        public ClientFM CreateOrUpdateClient(ClientFM clientFM)
        {
            try
            {
                var clientDetail = _context.Clients.Where(x => x.Id == clientFM.Id).FirstOrDefault();
                if (clientDetail != null)
                {
                    User user = new User();
                    user.Name = clientFM.User.Name;
                    user.EmailId = clientFM.User.EmailId;
                    user.MobileNo = clientFM.User.MobileNo;
                    user.Address = clientFM.User.Address;
                    user.City = clientFM.User.City;
                    user.Gender = clientFM.User.Gender;
                    user.Role = clientFM.User.Role;
                    user.Username = clientFM.User.Username;
                    user.ModifiedBy = clientFM.User.ModifiedBy;
                    user.Password = clientFM.User.Password;
                    user.ModifiedDate = DateTime.UtcNow;

                    _userRepository.UpdateUser(user);

                    Client client = new Client();
                    client.Id = clientDetail.Id;
                    client.UserId = user.Id;
                    client.State = clientDetail.State;
                    client.DateOfBirth = clientDetail.DateOfBirth;
                    client.AadharNumber = clientDetail.AadharNumber;
                    client.PanCardNumber = clientDetail.PanCardNumber;
                    client.VotingId = clientDetail.VotingId;
                    client.ModifiedDate = DateTime.UtcNow;
                    client.ModifiedBy = user.ModifiedBy;

                    _clientRepository.UpdateClient(client);
                }
                else
                {
                    User user = new User();
                    user.Name = clientFM.User.Name;
                    user.EmailId = clientFM.User.EmailId;
                    user.MobileNo = clientFM.User.MobileNo;
                    user.Address = clientFM.User.Address;
                    user.City = clientFM.User.City;
                    user.Gender = clientFM.User.Gender;
                    user.Role = clientFM.User.Role;
                    user.Username = clientFM.User.Username;
                    user.Password = clientFM.User.Password;
                    user.CreatedBy = clientFM.User.CreatedBy;
                    _userRepository.InsertUser(user);

                    Client client = new Client();
                    client.UserId = user.Id;
                    client.DateOfBirth = clientFM.DateOfBirth;
                    client.AadharNumber = clientFM.AadharNumber;
                    client.State = clientFM.State;
                    client.PanCardNumber = clientFM.PanCardNumber;
                    client.VotingId = clientFM.VotingId;
                    client.CreatedBy = user.CreatedBy;

                    _clientRepository.InsertClient(client);
                    clientFM.Id = client.Id;
                    clientFM.UserId = client.User.Id;
                }
            }
            catch (Exception ex)
            {

            }
            return clientFM;
        }

        public IEnumerable<Client> ListClientData()
        {
            var clientInfo = _clientRepository.GetClients().Where(x => x.IsDelete != true).ToList();
            // lawyerInfo.User = _userRepository.GetUser(lawyerInfo.UserId);
            return clientInfo;
        }

        public Client GetClientData(long clientId)
        {
            var clientDetail = _clientRepository.GetClient(clientId);
            return clientDetail;
        }

        public bool RemoveClient(long clientId)
        {
            var clientData = _clientRepository.GetClient(clientId);
            clientData.User = _userRepository.GetUser(clientData.UserId);
            if (clientData != null)
            {
                _clientRepository.DeleteClient(clientId);

                _userRepository.DeleteUser(clientData.User.Id);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
