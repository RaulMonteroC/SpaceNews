using Prism.Ioc;
using SpaceNews.Keys;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SpaceNews
{
    public partial class App
    {
        public App() : base(null) { }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage>();
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync(Pages.MainPage);
        }
    }
}