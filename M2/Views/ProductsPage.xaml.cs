using M2.ViewModels;
using M2.Views;
using M2.Models;
using M2.Services;

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

    private async void AddProduct_Clicked(
    object sender,
    EventArgs e)
    {
        await Navigation.PushAsync(
            new ProductEditPage());
    }

    private async void Products_SelectionChanged(
    object sender,
    SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count == 0)
            return;

        if (CurrentUser.RoleId != 1 &&
            CurrentUser.RoleId != 2)
        {
            ((CollectionView)sender).SelectedItem = null;
            return;
        }

        var product =
            e.CurrentSelection[0] as Product;

        if (product == null)
            return;

        await Navigation.PushAsync(
            new ProductEditPage(product));

        ((CollectionView)sender).SelectedItem = null;
    }
}