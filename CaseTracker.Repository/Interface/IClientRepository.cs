using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;
using CaseTracker.Data.ParameterModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseTracker.Repository.Interface
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetClients();
        Client GetClient(Guid id);
        Task<Paginate<Client>> GetClientsAsync(GetClientsParameters param);
        Task<bool> IsExistAsync(Guid id);
        Client InsertClient(Client client);
        void UpdateClient(Client client);
        void DeleteClient(Guid id);
        bool BulkDeleteClient(List<Guid> ids);
    }
}
