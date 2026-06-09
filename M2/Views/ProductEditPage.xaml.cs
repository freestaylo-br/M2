using Microsoft.Maui.Storage;
using M2.Models;
using M2.ViewModels;
using M2.Services;

namespace M2.Views;

public partial class ProductEditPage : ContentPage
{
    private string photoName = "picture";

    private FileResult? selectedImage;

    public ProductEditPage()
    {
        InitializeComponent();
    }

    private async void SelectPhoto_Clicked(
     object sender,
     EventArgs e)
    {
        var file = await FilePicker.Default.PickAsync(
            new PickOptions
            {
                PickerTitle = "Выберите изображение"
            });

        if (file == null)
            return;

        selectedImage = file;

        ProductImage.Source =
            ImageSource.FromFile(file.FullPath);
    }

    private async void Save_Clicked(
    object sender,
    EventArgs e)
    {
        try
        {
            var product = new Product
            {
                ProductName =
                    ProductNameEntry.Text,

                Description =
                    DescriptionEditor.Text,

                Amount =
                    decimal.Parse(
                        AmountEntry.Text),

                Count =
                    int.Parse(
                        CountEntry.Text),

                Discount =
                    int.Parse(
                        DiscountEntry.Text),

                Photo = ""
            };

            var api = new ApiService();

            var result =
                await api.CreateProductAsync(
                    product,
                    selectedImage);

            if (result)
            {
                await DisplayAlert(
                    "Успех",
                    "Товар добавлен",
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

    private Product currentProduct;

    public ProductEditPage(Product product)
    {
        InitializeComponent();

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

        if (!string.IsNullOrWhiteSpace(currentProduct.Photo))
        {
            ProductImage.Source =
                $"http://localhost:5156/images/{currentProduct.Photo}";
        }
        else
        {
            ProductImage.Source = "picture.jpg";
        }
    }
}