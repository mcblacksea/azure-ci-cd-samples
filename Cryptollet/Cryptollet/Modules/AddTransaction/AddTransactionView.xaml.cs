using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cryptollet.Modules.AddTransaction
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTransactionView : ContentPage
    {
        public AddTransactionView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<AddTransactionViewModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as AddTransactionViewModel).InitializeAsync(null);
        }
    }
}