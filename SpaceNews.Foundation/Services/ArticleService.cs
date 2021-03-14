using System.Collections.Generic;
using System.Threading.Tasks;
using Dawn;
using SpaceNews.Domain;
using SpaceNews.Foundation.Http;
using SpaceNews.Foundation.Http.APIs;

namespace SpaceNews.Foundation.Services
{
    public sealed class ArticleService : IArticleService
    {
        public ArticleService(IApiService<ISpaceFlightApi> apiService)
        {
            _apiService = Guard.Argument(apiService, nameof(apiService))
                               .NotNull()
                               .Value;
        }

        public Task<IEnumerable<Article>> GetArticles() => _apiService.Call(api => api.GetArticles());

        private readonly IApiService<ISpaceFlightApi> _apiService;
    }
}