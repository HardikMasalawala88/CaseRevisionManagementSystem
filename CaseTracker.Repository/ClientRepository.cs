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

namespace CaseTracker.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IRepository<Client> _clientRepository;
        public ClientRepository(IRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Task<bool> IsExistAsync(Guid id)
        {
            return _clientRepository.IsExistAsync(id);
        }

        public void DeleteClient(Guid id)
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
                data = data.Where(m => !string.IsNullOrEmpty(m.User.Firstname) 
                            && m.User.Name.Trim().ToLower().Contains(param.SearchStr)
                            || m.User.PhoneNumber.Trim().ToLower().Contains(param.SearchStr));
            }

            switch (param.SortLabel)
            {
                case "FullName":
                case "Name":
                    data = param.SortDirection == EnumListSortDirection.Ascending ? data.OrderBy(x => x.User.Name.Trim()) :
                     data.OrderByDescending(x => x.User.Name.Trim().ToString());
                    break;
                case "PhoneNumber":
                case "Phone":
                    data = param.SortDirection == EnumListSortDirection.Ascending ? data.OrderBy(x => x.User.PhoneNumber) : data.OrderByDescending(x => x.User.PhoneNumber);
                    break;
                default:
                    // Handle any unexpected sort label here
                    data = data.OrderByDescending(x => x.User.Name);
                    break;
            }

            result.TotalCount = data.Count();
            result.Data = data.Skip(skip).Take(param.PageSize).ToList();
            return result;
        }

        public bool BulkDeleteClient(List<Guid> ids)
        {
            var clientData = _clientRepository.GetAll().Where(x => ids.Contains(Guid.Parse(x.UserId))).ToList();

            clientData.ForEach(client =>
            {
                client.IsDelete = true;
                _clientRepository.Update(client);
            });

            return true;
        }

        public Client GetClient(Guid id)
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