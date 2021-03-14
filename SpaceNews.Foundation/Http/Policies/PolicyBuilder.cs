using Polly;

namespace SpaceNews.Foundation.Http.Policies
{
    internal sealed class PolicyBuilder
    {
        public PolicyBuilder()
        {
            _notFoundPolicy = new NotFoundPolicy();
            _retryPolicy = new RetryPolicy();
        }

        public IAsyncPolicy<TResult> Build<TResult>() => _notFoundPolicy.GeneratePolicy<TResult>().WrapAsync(_retryPolicy.GeneratePolicy<TResult>());
        public IAsyncPolicy Build() => _notFoundPolicy.GeneratePolicy().WrapAsync(_retryPolicy.GeneratePolicy());

        private readonly NotFoundPolicy _notFoundPolicy;
        private readonly RetryPolicy _retryPolicy;
    }
}