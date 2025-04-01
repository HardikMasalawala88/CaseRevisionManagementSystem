using CaseTracker.Data.ContextModels;
using CaseTracker.Data.FormModels;
using CaseTracker.Data.ParameterModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseTracker.Repository.Interface
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetClients();
        Client GetClient(long id);
        Task<Paginate<Client>> GetClientsAsync(GetClientsParameters param);
        Client InsertClient(Client client);
        void UpdateClient(Client client);
        void DeleteClient(long id);
        bool BulkDeleteClient(List<long> ids);
    }
}
