using Autofac;
using Cryptollet.Modules.AddTransaction;
using Xamarin.Forms;

namespace Cryptollet
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<AppShellViewModel>();

            Routing.RegisterRoute("AddTransactionViewModel", typeof(AddTransactionView));
        }
    }
}
