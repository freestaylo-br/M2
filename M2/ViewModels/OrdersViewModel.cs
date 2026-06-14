using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using M2.Models;
using M2.Services;

namespace M2.ViewModels;

public class OrdersViewModel : INotifyPropertyChanged
{
    private readonly ApiService _api =
        new ApiService();

    private ObservableCollection<Order> orders =
        new();

    public ObservableCollection<Order> Orders
    {
        get => orders;
        set
        {
            orders = value;
            OnPropertyChanged();
        }
    }

    public bool IsAdmin =>
        CurrentUser.RoleId == 1;

    public bool IsManager =>
        CurrentUser.RoleId == 2;

    public bool IsClient =>
        CurrentUser.RoleId == 3;

    public OrdersViewModel()
    {
        _ = LoadOrders();
    }

    public async Task LoadOrders()
    {
        var data =
            await _api.GetOrders();

        Orders =
            new ObservableCollection<Order>(
                data);
    }

    public event PropertyChangedEventHandler?
        PropertyChanged;

    private void OnPropertyChanged(
        [CallerMemberName]
        string propertyName = "")
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(
                propertyName));
    }
}