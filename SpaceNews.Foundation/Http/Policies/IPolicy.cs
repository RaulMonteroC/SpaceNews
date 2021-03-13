using Polly;

namespace SpaceNews.Foundation.Http.Policies
{
    public interface IPolicy
    {
        IAsyncPolicy GeneratePolicy();
        IAsyncPolicy<TResult> GeneratePolicy<TResult>();
    }
}