using CMS.Data.ContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repository.Interface
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetClients();
        Client GetClient(long id);
        Client InsertClient(Client client);
        void UpdateClient(Client client);
        void DeleteClient(long id);
    }
}
