using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Inventory.Helpers;
using Inventory.Models;
using System;

namespace Inventory.Services
{
    public class SqliteService : ISqliteService
    {
        private static readonly AsyncLock Mutex = new AsyncLock();
        private SQLiteAsyncConnection _sqlCon;

        public SqliteService()
        {
            var databasePath = DependencyService.Get<IPathService>().GetDatabasePath();
            _sqlCon = new SQLiteAsyncConnection(databasePath);

            CreateDatabaseAsync();
            CreateDataMock();
        }

        private void CreateDataMock()
        {
            List<Item> Items = new List<Item>(){
                new Item { Id = 1746488947553, Text = "Computador ASUS", Description = "Este es un producto delicado", Quantity = 5 },
                new Item { Id = 1746488947554, Text = "Pantalla LG", Description = "Tecnologia OLED", Quantity = 25 },
                new Item { Id = 1746488947555, Text = "Celular Nokia", Description = "Articulo usado", Quantity = 2 },
                new Item { Id = 1746488947556, Text = "Audifonos", Description = "In-ear", Quantity = 0 },
                new Item { Id = 1746488947557, Text = "Teclado", Description = "Mecanico", Quantity = 1 },
                new Item { Id = 1746488947558, Text = "Mouse", Description = "Ergonomico", Quantity = 15 },
                new Item { Id = 1746488947559, Text = "Laptop HP", Description = "Articulo usado", Quantity = 0 }
            };

            foreach (var item in Items)
            {
                Insert(item);
            }
        }

        public async void CreateDatabaseAsync()
        {
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                await _sqlCon.CreateTableAsync<Item>().ConfigureAwait(false);
            }
        }

        public async Task<IList<Item>> GetAll()
        {
            var items = new List<Item>();
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                items = await _sqlCon.Table<Item>().ToListAsync().ConfigureAwait(false);
            }

            return items;
        }

        public async Task Insert(Item item)
        {
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                var existingTodoItem = await _sqlCon.Table<Item>()
                        .Where(x => x.Id == item.Id)
                        .FirstOrDefaultAsync();

                if (existingTodoItem == null)
                {
                    await _sqlCon.InsertAsync(item).ConfigureAwait(false);
                }
                else
                {
                    item.Id = existingTodoItem.Id;
                    await _sqlCon.UpdateAsync(item).ConfigureAwait(false);
                }
            }
        }

        public async Task Remove(Item item)
        {
            await _sqlCon.DeleteAsync(item);
        }

        public async Task<Item> GetItem(long id)
        {
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                var item = await _sqlCon.Table<Item>()
                                        .Where(x => x.Id == id)
                                        .FirstOrDefaultAsync()
                                        .ConfigureAwait(false);
                return item;
            }
        }

    }
}
