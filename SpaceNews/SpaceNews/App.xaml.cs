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
            // Navigation Registration
            containerRegistry.RegisterForNavigation<MainPage>();

            // Services


        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync(Pages.MainPage);
        }
    }
}