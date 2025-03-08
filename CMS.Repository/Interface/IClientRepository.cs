using CMS.Data.ContextModels;
using CMS.Data.FormModels;
using CMS.Data.ParameterModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Repository.Interface
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
