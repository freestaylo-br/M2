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

    protected void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(
                propertyName));
    }
}