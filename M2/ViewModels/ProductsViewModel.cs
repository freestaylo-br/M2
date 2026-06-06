using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using M2.Models;
using M2.Services;

namespace M2.ViewModels;

public class ProductsViewModel : INotifyPropertyChanged
{
    private readonly ApiService _api = new ApiService();

    private string userName = "";
    private string searchText = "";
    private bool isAscending = true;

    private ObservableCollection<Product> products = new();

    private List<Product> allProducts = new();

    public bool CanSort => CurrentUser.RoleId != 0;

    public bool IsAdmin => CurrentUser.RoleId == 1;

    public bool IsManager => CurrentUser.RoleId == 2;

    public bool IsClient => CurrentUser.RoleId == 3;

    public bool IsGuest => CurrentUser.RoleId == 0;

    public string UserName
    {
        get => userName;
        set
        {
            userName = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Product> Products
    {
        get => products;
        set
        {
            products = value;
            OnPropertyChanged();
        }
    }

    public ProductsViewModel()
    {
        UserName = CurrentUser.FullName;

        LoadProducts();
    }

    private async void LoadProducts()
    {
        var data = await _api.GetProducts();

        allProducts = data;

        Products = new ObservableCollection<Product>(data);
    }

    public void ToggleSort()
    {
        if (isAscending)
        {
            Products = new ObservableCollection<Product>(
                Products.OrderBy(x => x.Count));
        }
        else
        {
            Products = new ObservableCollection<Product>(
                Products.OrderByDescending(x => x.Count));
        }

        isAscending = !isAscending;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(
        [CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}