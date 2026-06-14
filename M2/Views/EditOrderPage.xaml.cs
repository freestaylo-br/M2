using M2.Models;
using M2.Services;

namespace M2.Views;

public partial class EditOrderPage : ContentPage
{
    private readonly ApiService _api =
        new ApiService();

    private Order? currentOrder;

    private bool isEditMode;

    private List<Status> statuses = new();

    private List<PickupLocation> locations = new();

    private List<Product> products = new();

    public EditOrderPage()
    {
        InitializeComponent();

        TitleLabel.Text =
            "Добавление заказа";

        DeleteButton.IsVisible = false;

        _ = LoadData();
    }

    public EditOrderPage(Order order)
    {
        InitializeComponent();

        currentOrder = order;

        isEditMode = true;

        TitleLabel.Text =
            "Редактирование заказа";

        DeleteButton.IsVisible = true;

        _ = LoadData();
    }

    private async void Back_Clicked(
    object sender,
    EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async Task LoadData()
    {
        statuses =
            await _api.GetStatuses();

        StatusPicker.ItemsSource =
            statuses;

        StatusPicker.ItemDisplayBinding =
            new Binding("StatusName");

        locations =
            await _api.GetPickupLocations();

        LocationPicker.ItemsSource =
            locations;

        LocationPicker.ItemDisplayBinding =
            new Binding("Street");

        products =
            await _api.GetProductsForOrder();

        ProductPicker.ItemsSource =
            products;

        ProductPicker.ItemDisplayBinding =
            new Binding("Article");

        if (currentOrder != null)
        {
            StatusPicker.SelectedItem =
                statuses.FirstOrDefault(
                    x => x.StatusId ==
                         currentOrder.StatusId);

            LocationPicker.SelectedItem =
                locations.FirstOrDefault(
                    x => x.LocationId ==
                         currentOrder.LocationId);

            ProductPicker.SelectedItem =
                products.FirstOrDefault(
                    x => x.ProductId ==
                         currentOrder.ProductId);

            OrderDatePicker.Date =
                currentOrder.OrderDate;

            DeliveryDatePicker.Date =
                currentOrder.DeliveryDate;
        }
    }

    private async void Save_Clicked(
        object sender,
        EventArgs e)
    {
        try
        {
            var selectedStatus =
                StatusPicker.SelectedItem
                as Status;

            var selectedLocation =
                LocationPicker.SelectedItem
                as PickupLocation;

            var selectedProduct =
                ProductPicker.SelectedItem
                as Product;

            if (selectedStatus == null)
            {
                await DisplayAlert(
                    "Ошибка",
                    "Выберите статус",
                    "OK");
                return;
            }

            if (selectedLocation == null)
            {
                await DisplayAlert(
                    "Ошибка",
                    "Выберите ПВЗ",
                    "OK");
                return;
            }

            if (selectedProduct == null)
            {
                await DisplayAlert(
                    "Ошибка",
                    "Выберите товар",
                    "OK");
                return;
            }

            var order = new Order
            {
                OrderId =
                    currentOrder?.OrderId ?? 0,

                ProductId =
                    selectedProduct.ProductId,

                Article =
                    selectedProduct.Article,

                StatusId =
                    selectedStatus.StatusId,

                StatusName =
                    selectedStatus.StatusName,

                LocationId =
                    selectedLocation.LocationId,

                PickupLocation =
                    $"{selectedLocation.City}, " +
                    $"{selectedLocation.Street}, " +
                    $"{selectedLocation.Home}",

                OrderDate =
                    OrderDatePicker.Date,

                DeliveryDate =
                    DeliveryDatePicker.Date
            };

            bool result;

            if (isEditMode)
            {
                result =
                    await _api.UpdateOrderAsync(
                        order);
            }
            else
            {
                result =
                    await _api.CreateOrderAsync(
                        order);
            }

            if (result)
            {
                await DisplayAlert(
                    "Успех",
                    "Заказ сохранен",
                    "OK");

                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert(
                    "Ошибка",
                    "Не удалось сохранить заказ",
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

    private async void Delete_Clicked(
        object sender,
        EventArgs e)
    {
        if (currentOrder == null)
            return;

        bool confirm =
            await DisplayAlert(
                "Удаление",
                "Удалить заказ?",
                "Да",
                "Нет");

        if (!confirm)
            return;

        var result =
            await _api.DeleteOrderAsync(
                currentOrder.OrderId);

        if (result)
        {
            await DisplayAlert(
                "Успех",
                "Заказ удален",
                "OK");

            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert(
                "Ошибка",
                "Не удалось удалить заказ",
                "OK");
        }
    }
}