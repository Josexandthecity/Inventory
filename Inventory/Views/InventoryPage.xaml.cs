using Inventory.ViewModels;
using Xamarin.Forms;

namespace Inventory.Views
{
    public partial class InventoryPage : ContentPage
    {
        InventoryViewModel _viewModel;

        public InventoryPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new InventoryViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}