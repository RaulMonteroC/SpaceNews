using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
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

            NavigateToDetailCommand = ReactiveCommand.CreateFromTask(ExecuteNavigateToDetail);
            LoadArticlesCommand = ReactiveCommand.CreateFromTask(ExecuteLoadArticles);
            FetchMoreArticlesCommand = ReactiveCommand.CreateFromTask(ExecuteLoadArticles);

            LoadArticlesCommand.Execute()
                .Subscribe()
                .DisposeWith(Disposables);

            LoadArticlesCommand.Subscribe(articles => ArticlesSourceCache.AddOrUpdate(articles))
                               .DisposeWith(Disposables);

            LoadArticlesCommand.IsExecuting.ToProperty(this, nameof(IsBusy), out _isBusy)
                                           .DisposeWith(Disposables);

            FetchMoreArticlesCommand.Subscribe(articles => ArticlesSourceCache.AddOrUpdate(articles))
                                    .DisposeWith(Disposables);

            FetchMoreArticlesCommand.IsExecuting.ToProperty(this, nameof(IsLoadingMoreArticles), out _isLoadingMoreArticles)
                                                .DisposeWith(Disposables);
        }

        public bool IsBusy => _isBusy.Value;
        public bool IsLoadingMoreArticles => _isLoadingMoreArticles.Value;
        public SourceCache<Article, string> ArticlesSourceCache { get; } = new SourceCache<Article, string>(x => x.Id);
        public ReadOnlyObservableCollection<Article> Articles => _articles;

        public ReactiveCommand<Unit, IEnumerable<Article>> LoadArticlesCommand { get; }
        public ReactiveCommand<Unit, IEnumerable<Article>> FetchMoreArticlesCommand { get; }
        public ReactiveCommand <Unit, Unit> NavigateToDetailCommand { get; }

        private Task<IEnumerable<Article>> ExecuteLoadArticles() => _articleService.GetArticles(Articles.Count,NumberOfArticlesPerFetch);
        private Task ExecuteNavigateToDetail() => _navigationService.NavigateAsync(Pages.MainPage);

        private const int NumberOfArticlesPerFetch = 10;
        private readonly ReadOnlyObservableCollection<Article> _articles;
        private readonly IArticleService _articleService;
        private readonly INavigationService _navigationService;
        private readonly ObservableAsPropertyHelper<bool> _isBusy;
        private readonly ObservableAsPropertyHelper<bool> _isLoadingMoreArticles;
    }
}