using M2.ViewModels;

namespace M2.Views;

public partial class ProductsPage : ContentPage
{
    public ProductsPage()
    {
        InitializeComponent();

        BindingContext = new ProductsViewModel();
    }

    private async void ExitClicked(
        object sender,
        EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

    private void SortClicked(object sender, EventArgs e)
    {
        if (BindingContext is ProductsViewModel vm)
        {
            vm.ToggleSort();
        }
    }
}