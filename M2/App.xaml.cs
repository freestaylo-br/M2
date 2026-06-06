using M2.Views;

namespace M2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(
                new LoginPage());
        }
    }
}