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
        private readonly IAppUserRepository _userRepository;
        private readonly ApplicationContext _context;

        public ClientService(IClientRepository clientRepository, ApplicationContext context, IAppUserRepository userRepository)
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
                    ApplicationUser user = new ApplicationUser();
                    user.Name = clientFM.ApplicationUser.Name;
                    user.Email = clientFM.ApplicationUser.Email;
                    user.PhoneNumber = clientFM.ApplicationUser.PhoneNumber;
                    user.Address = clientFM.ApplicationUser.Address;
                    user.City = clientFM.ApplicationUser.City;
                    user.Gender = clientFM.ApplicationUser.Gender;
                    user.Role = clientFM.ApplicationUser.Role;
                    user.UserName = clientFM.ApplicationUser.UserName;
                    user.ModifiedBy = clientFM.ApplicationUser.ModifiedBy;
                    user.PhoneNumber = clientFM.ApplicationUser.PasswordHash;
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
                    ApplicationUser user = new ApplicationUser();
                    user.Name = clientFM.ApplicationUser.Name;
                    user.Email = clientFM.ApplicationUser.Email;
                    user.PhoneNumber = clientFM.ApplicationUser.PhoneNumber;
                    user.Address = clientFM.ApplicationUser.Address;
                    user.City = clientFM.ApplicationUser.City;
                    user.Gender = clientFM.ApplicationUser.Gender;
                    user.Role = clientFM.ApplicationUser.Role;
                    user.UserName = clientFM.ApplicationUser.UserName;
                    user.ModifiedBy = clientFM.ApplicationUser.ModifiedBy;
                    user.PhoneNumber = clientFM.ApplicationUser.PasswordHash;
                    user.ModifiedDate = DateTime.UtcNow;
                    user.CreatedBy = clientFM.ApplicationUser.CreatedBy;
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
                    clientFM.UserId = client.ApplicationUser.Id;
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
            clientData.ApplicationUser = _userRepository.GetUser(clientData.UserId);
            if (clientData != null)
            {
                _clientRepository.DeleteClient(clientId);

                _userRepository.DeleteUser(clientData.ApplicationUser.Id);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
