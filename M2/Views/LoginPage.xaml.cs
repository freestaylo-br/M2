using M2.Services;
using M2.ViewModels;

namespace M2.Views;

public partial class LoginPage : ContentPage
{
    private readonly ApiService _api =
        new ApiService();

    public LoginPage()
    {
        InitializeComponent();

        BindingContext = new LoginViewModel();
    }

    private async void LoginClicked(
        object sender,
        EventArgs e)
    {
        var vm =
            BindingContext as LoginViewModel;

        var user =
            await _api.Login(
                vm.Login,
                vm.Password);

        if (user == null)
        {
            await DisplayAlert(
                "Ошибка",
                "Неверный логин или пароль",
                "ОК");

            return;
        }

        CurrentUser.FullName =
            $"{user.Surname} {user.Name} {user.Patronymic}";

        CurrentUser.RoleId = user.RoleId;

        await Navigation.PushAsync(
            new ProductsPage());
    }

    private async void GuestClicked(
        object sender,
        EventArgs e)
    {
        CurrentUser.FullName = "Гость";
        CurrentUser.RoleId = 0;

        await Navigation.PushAsync(
            new ProductsPage());
    }
}