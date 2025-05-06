using Inventory.Models;
using Inventory.ViewModels;
using Xamarin.Forms;

namespace Inventory.Views
{
    public partial class MerchandisePage : ContentPage
    {
        public MerchandisePage()
        {
            InitializeComponent();
        }

        private void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (BindingContext is MerchandiseViewModel viewModel && viewModel.SaveCommand.CanExecute(null))
            {
                if (sender is BindableObject bindableObject && bindableObject.BindingContext is Item item)
                {
                    viewModel.SaveCommand.Execute(item);
                }
            }
        }

        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            if (BindingContext is MerchandiseViewModel viewModel && viewModel.SearchItemsCommand.CanExecute(null))
            {
                viewModel.SearchItemsCommand.Execute(e.NewTextValue);
            }
        }
    }
}