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
}