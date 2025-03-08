using CMS.Data.ContextModels;
using CMS.Data.Enum;
using CMS.Data.FormModels;
using CMS.Data.ParameterModels;
using CMS.Repository.Interface;
using CMS.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IRepository<Client> _clientRepository;
        public ClientRepository(IRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public void DeleteClient(long id)
        {
            Client client = GetClient(id);
            client.IsDelete = true;
            client.ModifiedDate = DateTime.UtcNow;
            client.ModifiedBy = client.CreatedBy;

            _clientRepository.SaveChanges();
        }

        public async Task<Paginate<Client>> GetClientsAsync(GetClientsParameters param)
        {
            var result = new Paginate<Client>();

            if (param == null)
            {
                return result;
            }

            var skip = param.PageSize * (param.Page - 1);

            var data = GetClients()
                      .Where(x => !x.IsDelete).AsQueryable();

            if (!string.IsNullOrEmpty(param.SearchStr))
            {
                param.SearchStr = param.SearchStr.Trim().ToLower();
                data = data.Where(m => !string.IsNullOrEmpty(m.User.Name) && m.User.Name.Trim().ToLower().Contains(param.SearchStr)
                            || m.User.MobileNo.Trim().ToLower().Contains(param.SearchStr));
            }

            switch (param.SortLabel)
            {
                case "FullName":
                case "Name":
                    data = param.SortDirection == EnumListSortDirection.Ascending ? data.OrderBy(x => x.User.Name.Trim()) : data.OrderByDescending(x => string.Concat(x.User.Name).Trim());
                    break;
                case "PhoneNumber":
                case "Phone":
                    data = param.SortDirection == EnumListSortDirection.Ascending ? data.OrderBy(x => x.User.MobileNo) : data.OrderByDescending(x => x.User.MobileNo);
                    break;
                default:
                    // Handle any unexpected sort label here
                    data = data.OrderByDescending(x => x.Id);
                    break;
            }

            result.TotalCount = data.Count();
            result.Data = data.Skip(skip).Take(param.PageSize).Select(client => new Client
            {
                Id = client.Id,
                AadharNumber = client.AadharNumber,
                PanCardNumber = client.PanCardNumber,
                VotingId = client.VotingId,
                State = client.State,
                UserId = client.UserId,
                DateOfBirth = client.DateOfBirth,
                CreatedDate = client.CreatedDate,
            }).ToList();

            return result;
        }

        public bool BulkDeleteClient(List<long> ids)
        {
            var clientData = _clientRepository.GetAll().Where(x => ids.Contains(x.Id)).ToList();

            clientData.ForEach(client =>
            {
                client.IsDelete = true;
                _clientRepository.Update(client);
            });

            return true;
        }

        public Client GetClient(long id)
        {
            return _clientRepository.GetById(id);
        }

        public IEnumerable<Client> GetClients()
        {
            return _clientRepository.GetAll();
        }

        public Client InsertClient(Client client)
        {
            Client clientData = _clientRepository.Insert(client);
            return clientData;
        }

        public void UpdateClient(Client client)
        {
            _clientRepository.Update(client);
        }
    }
}