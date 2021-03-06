using Prism.Ioc;
using SpaceNews.Foundation.Http;
using SpaceNews.Keys;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SpaceNews
{
    public partial class App
    {
        public App() : base(null) { }

        protected override void RegisterTypes(IContainerRegistry container)
        {
            // Navigation Registration
            container.RegisterForNavigation<MainPage>();

            // Services
            container.RegisterSingleton(typeof(IApiService<>), typeof(ApiService<>));

        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync(Pages.MainPage);
        }
    }
}