using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace M2.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private string login;

    private string password;

    public string Login
    {
        get => login;
        set
        {
            login = value;
            OnPropertyChanged();
        }
    }

    public string Password
    {
        get => password;
        set
        {
            password = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler?
        PropertyChanged;

    private void OnPropertyChanged(
        [CallerMemberName]
        string propertyName = "")
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}