using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using SpaceNews.Domain;
using SpaceNews.Foundation.Attributes;
using SpaceNews.Foundation.Constants;

namespace SpaceNews.Foundation.Http.APIs
{
    [Url(AppConstants.BaseApiUrl)]
    [Headers("Accept: application/json")]
    public interface ISpaceFlightApi
    {
        [Get("/articles?_start={start}&&_limit={limit}")]
        Task<IEnumerable<Article>> GetArticles(int start, int limit);
    }
}