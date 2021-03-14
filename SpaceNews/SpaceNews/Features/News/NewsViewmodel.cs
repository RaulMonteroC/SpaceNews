using System.Collections.ObjectModel;
using DynamicData;
using SpaceNews.Domain;

namespace SpaceNews.Features.News
{
    public sealed class NewsViewmodel : BaseViewModel
    {
        public NewsViewmodel()
        {
            ArticlesSourceCache.Connect()
                .Bind(out _articles)
                .DisposeMany();
        }

        public SourceCache<Article, string> ArticlesSourceCache { get; } = new SourceCache<Article, string>(x => x.Id);
        public ReadOnlyObservableCollection<Article> Articles => _articles;

        private readonly ReadOnlyObservableCollection<Article> _articles;
    }
}