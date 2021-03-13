using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Refit;
using Polly;

namespace SpaceNews.Foundation.Http.Policies
{
    public class NotFoundPolicy : IPolicy
    {
        
        public IAsyncPolicy GeneratePolicy()
        {
            return Policy.Handle<ApiException>(m => m.StatusCode == HttpStatusCode.NotFound)
                .FallbackAsync((token) =>
                {
                    Debug.WriteLine("Error: Resource not found on server");

                    return Task.CompletedTask;
                });
        }     
        
        public IAsyncPolicy<TResult> GeneratePolicy<TResult>()
        {
            return Policy<TResult>.Handle<ApiException>(m => m.StatusCode == HttpStatusCode.NotFound)
                .FallbackAsync((token) =>
                {
                    Debug.WriteLine("Error: Resource not found on server");

                    return Task.FromResult(default(TResult));
                });
        }
    }
}