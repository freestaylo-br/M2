using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using M2.Models;
using M2.Services;

namespace M2.ViewModels;

public class ProductsViewModel : INotifyPropertyChanged
{
    private readonly ApiService _api = new ApiService();

    private string userName = "";

    private string searchStr = "";

    private Supplier? selectedSupplier;

    private bool sortDescending;

    public string supplier_name;


    
    public bool CanSearch => CurrentUser.RoleId == 1 ||
        CurrentUser.RoleId == 2;

    private ObservableCollection<Product> products = new();

    private List<Product> allProducts = new();
    
    public ObservableCollection<Supplier> Suppliers
    { get; set; } = new();

    public bool CanSort => CurrentUser.RoleId == 1 ||
        CurrentUser.RoleId == 2;

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

    public string SearchStr
    {
        get => searchStr;
        set
        {
            searchStr = value;
            OnPropertyChanged();

            LoadProducts();
        }
    }

    private async Task LoadSuppliers()
    {
        var suppliers = await _api.GetSuppliers();

        Suppliers.Clear();

        Suppliers.Add(new Supplier
        {
            SupplierId = 0,
            supplier_name = "Все поставщики"
        });

        foreach (var supplier in suppliers)
        {
            Suppliers.Add(supplier);
        }

        SelectedSupplier = Suppliers[0];
    }

    public Supplier? SelectedSupplier
    {
        get => selectedSupplier;
        set
        {
            selectedSupplier = value;
            OnPropertyChanged();

            LoadProducts();
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

        _ = LoadSuppliers();

        LoadProducts();
    }

    public bool SortDescending
    {
        get => sortDescending;
        set
        {
            if (sortDescending == value) return;

            sortDescending = value;

            OnPropertyChanged();

            _ = LoadProducts();
        }
    }

    public async Task LoadProducts()
    {
        var data = await _api.GetProducts(
            SearchStr,
            SortDescending,
            SelectedSupplier?.SupplierId == 0
                ? null
                : SelectedSupplier?.SupplierId);

        Products = new ObservableCollection<Product>(data);
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