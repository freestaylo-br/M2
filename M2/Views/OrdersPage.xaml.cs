using M2.Models;
using M2.Services;
using M2.ViewModels;

namespace M2.Views;

public partial class OrdersPage : ContentPage
{
    private readonly OrdersViewModel vm;

    public OrdersPage()
    {
        InitializeComponent();

        vm = new OrdersViewModel();

        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await vm.LoadOrders();
    }

    private async void AddOrder_Clicked(
        object sender,
        EventArgs e)
    {
        await Navigation.PushAsync(
            new EditOrderPage());
    }

    private async void Orders_SelectionChanged(
        object sender,
        SelectionChangedEventArgs e)
    {
        var order =
            e.CurrentSelection
                .FirstOrDefault() as Order;

        if (order == null)
            return;

        OrdersCollection.SelectedItem = null;

        if (CurrentUser.RoleId != 1)
            return;

        await Navigation.PushAsync(
            new EditOrderPage(order));
    }

    private async void Back_Clicked(
        object sender,
        EventArgs e)
    {
        await Navigation.PopAsync();
    }
}