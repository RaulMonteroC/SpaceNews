using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Dawn;
using DynamicData;
using Prism.Navigation;
using ReactiveUI;
using SpaceNews.Domain;
using SpaceNews.Foundation.Services;
using SpaceNews.Keys;

namespace SpaceNews.Features.News
{
    public sealed class NewsViewModel : BaseViewModel
    {
        public NewsViewModel(IArticleService articleService,
            INavigationService navigationService)
        {
            _articleService = Guard.Argument(articleService, nameof(articleService))
                .NotNull()
                .Value;

            _navigationService = Guard.Argument(navigationService, nameof(navigationService))
                .NotNull()
                .Value;

            ArticlesSourceCache.Connect()
                .Bind(out _articles)
                .DisposeMany()
                .Subscribe();

            LoadArticlesCommand = ReactiveCommand.CreateFromTask(ExecuteLoadArticles);
            NavigateToDetailCommand = ReactiveCommand.CreateFromTask(ExecuteNavigateToDetail);

            LoadArticlesCommand.Execute()
                .Subscribe(articles => ArticlesSourceCache.AddOrUpdate(articles))
                .DisposeWith(Disposables);

        }

        public SourceCache<Article, string> ArticlesSourceCache { get; } = new SourceCache<Article, string>(x => x.Id);
        public ReadOnlyObservableCollection<Article> Articles => _articles;

        public ReactiveCommand<Unit, IEnumerable<Article>> LoadArticlesCommand { get; }
        public ReactiveCommand <Unit, Unit> NavigateToDetailCommand { get; }

        private Task<IEnumerable<Article>> ExecuteLoadArticles() => _articleService.GetArticles();
        private Task ExecuteNavigateToDetail() => _navigationService.NavigateAsync(Pages.MainPage);

        private readonly ReadOnlyObservableCollection<Article> _articles;
        private readonly IArticleService _articleService;
        private readonly INavigationService _navigationService;
    }
}