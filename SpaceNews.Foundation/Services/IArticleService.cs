using System.Collections.Generic;
using System.Threading.Tasks;
using SpaceNews.Domain;

namespace SpaceNews.Foundation.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetArticles(int start, int limit);
    }
}