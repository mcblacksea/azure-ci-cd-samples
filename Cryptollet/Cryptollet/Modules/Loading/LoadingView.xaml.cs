using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cryptollet.Modules.Loading
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingView : ContentPage
    {
        public LoadingView()
        {
            InitializeComponent();
            BindingContext = App.Container.Resolve<LoadingViewModel>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as LoadingViewModel).InitializeAsync(null);
        }
    }
}