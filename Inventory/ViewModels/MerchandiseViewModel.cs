using Inventory.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Inventory.ViewModels
{
    public class MerchandiseViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; }

        public Command<Item> ItemTapped { get; }
        public Command<string> SearchItemsCommand { get; }

        public Command SaveCommand { get; }
        public MerchandiseViewModel()
        {
            Title = "Registro de Mercancias";
            Items = new ObservableCollection<Item>();
            ItemTapped = new Command<Item>(ExecuteItemSelected);
            SearchItemsCommand = new Command<string>(async (query) => await ExecuteSearchItem(query));
            SaveCommand = new Command<Item>(ExecuteSave);
            IsBusy = true;
        }

        async Task ExecuteSearchItem(string query)
        {
            try
            {
                Items.Clear();
                if (query == null) { query = ""; }
                var result = await DataStore.GetAll();
                foreach (var item in result)
                {
                    if (query != null && item.Text.ToLower().Contains(query.ToLower()) || item.Description.ToLower().Contains(query.ToLower()))
                    {
                        Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsVisible = false;
                IsBusy = false;
            }
        }

        void ExecuteItemSelected(Item item)
        {
            if (item == null)
                return;

            IsVisible = !IsVisible;
        }

        async void ExecuteSave(Item item)
        {
            await DataStore.Insert(item);
        }
    }
}