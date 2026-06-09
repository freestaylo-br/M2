using Microsoft.Maui.Storage;
using M2.Models;
using M2.Services;

namespace M2.Views;

public partial class ProductEditPage : ContentPage
{
    private readonly ApiService _api = new ApiService();

private Product? currentProduct;

    private FileResult? selectedImage;

    private List<Manufacturer> manufacturers = new();

    private List<Supplier> suppliers = new();

    private List<Category> categories = new();

    private string photoName = "picture";

    public ProductEditPage()
    {
        InitializeComponent();

        _ = LoadPickers();
    }

    public ProductEditPage(Product product)
    {
        InitializeComponent();

        _ = LoadPickers();

        currentProduct = product;

        ProductNameEntry.Text =
            product.ProductName;

        DescriptionEditor.Text =
            product.Description;

        AmountEntry.Text =
            product.Amount.ToString();

        CountEntry.Text =
            product.Count.ToString();

        DiscountEntry.Text =
            product.Discount.ToString();

        if (!string.IsNullOrWhiteSpace(product.Photo))
        {
            ProductImage.Source =
                $"http://localhost:5156/images/{product.Photo}";
        }
        else
        {
            ProductImage.Source = "picture.jpg";
        }
    }

    private async Task LoadPickers()
    {
        manufacturers =
            await _api.GetManufacturers();

        ManufacturerPicker.ItemsSource =
            manufacturers;

        ManufacturerPicker.ItemDisplayBinding =
            new Binding("ManufacturerName");

        suppliers =
            await _api.GetSuppliers();

        SupplierPicker.ItemsSource =
            suppliers;

        SupplierPicker.ItemDisplayBinding =
            new Binding("supplier_name");

        categories = await _api.GetCategories();

        CategoryPicker.ItemsSource = categories;

        CategoryPicker.ItemDisplayBinding = new Binding("CategoryName");

        if (currentProduct != null)
        {
            ManufacturerPicker.SelectedItem =
                manufacturers.FirstOrDefault(
                    x => x.ManufacturerName ==
                         currentProduct.Manufacturer);

            SupplierPicker.SelectedItem =
                suppliers.FirstOrDefault(
                    x => x.supplier_name ==
                         currentProduct.Supplier);

            CategoryPicker.SelectedItem = suppliers.FirstOrDefault(
                x => x.supplier_name == currentProduct.Category);
        }
    }

    private async void SelectPhoto_Clicked(
        object sender,
        EventArgs e)
    {
        var file =
            await FilePicker.Default.PickAsync(
                new PickOptions
                {
                    PickerTitle =
                        "Выберите изображение"
                });

        if (file == null)
            return;

        selectedImage = file;

        ProductImage.Source =
            ImageSource.FromFile(
                file.FullPath);
    }

    private async void Save_Clicked(
        object sender,
        EventArgs e)
    {
        try
        {
            var product = new Product
            {
                ProductId =
                    currentProduct?.ProductId ?? 0,

                ProductName =
                    ProductNameEntry.Text ?? "",

                Description =
                    DescriptionEditor.Text ?? "",

                Amount =
                    decimal.Parse(
                        AmountEntry.Text ?? "0"),

                Count =
                    int.Parse(
                        CountEntry.Text ?? "0"),

                Discount =
                    decimal.Parse(
                        DiscountEntry.Text ?? "0"),

                Manufacturer =
                    (ManufacturerPicker.SelectedItem
                        as Manufacturer)
                        ?.ManufacturerName ?? "",

                Supplier =
                    (SupplierPicker.SelectedItem
                        as Supplier)
                        ?.supplier_name ?? "",

                Category = 
                    (CategoryPicker.SelectedItem
                        as Category)
                        ?.CategoryName ?? "",

                Measurement =
                    currentProduct?.Measurement ?? "",

                Article =
                    currentProduct?.Article ?? "",

                Photo =
                    currentProduct?.Photo
            };

            var result =
                await _api.CreateProductAsync(
                    product,
                    selectedImage);

            if (result)
            {
                await DisplayAlert(
                    "Успех",
                    "Товар сохранен",
                    "OK");

                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert(
                    "Ошибка",
                    "Не удалось сохранить товар",
                    "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Ошибка",
                ex.Message,
                "OK");
        }
    }

    private async void Back_Clicked(
        object sender,
        EventArgs e)
    {
        await Navigation.PopAsync();
    }
}