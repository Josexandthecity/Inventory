using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{
    public interface ISqliteService
    {
        Task<IList<Item>> GetAll();
        Task Insert(Item item);
        Task Remove(Item item);

        Task<Item> GetItem(long id);
    }
}
