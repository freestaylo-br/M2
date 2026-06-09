using M2.Views;
namespace M2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(
                nameof(LoginPage),
                typeof(LoginPage));

            Routing.RegisterRoute(
                nameof(ProductsPage),
                typeof(ProductsPage));

            Routing.RegisterRoute(
                nameof(ProductEditPage),
                typeof(ProductEditPage));
        }
    }
}
