using Inventory.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Inventory.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}