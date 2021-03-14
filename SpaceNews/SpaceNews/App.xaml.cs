using Prism.Ioc;
using SpaceNews.Features.News;
using SpaceNews.Foundation.Http;
using SpaceNews.Foundation.Services;
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
            container.RegisterForNavigation<MainPage>(Pages.MainPage);
            container.RegisterForNavigation<NewsPage, NewsViewModel>(Pages.NewsPage);

            // Services
            container.RegisterSingleton(typeof(IApiService<>), typeof(ApiService<>));
            container.Register<IArticleService, ArticleService>();

        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync(Pages.NewsPage);
        }
    }
}