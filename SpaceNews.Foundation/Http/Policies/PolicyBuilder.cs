using Polly;

namespace SpaceNews.Foundation.Http.Policies
{
    public class PolicyBuilder
    {
        private readonly NotFoundPolicy _notFoundPolicy;
        private readonly RetryPolicy _retryPolicy;

        public PolicyBuilder()
        {
            _notFoundPolicy = new NotFoundPolicy();
            _retryPolicy = new RetryPolicy();
        }

        public IAsyncPolicy<TResult> Build<TResult>()
        {
            return _notFoundPolicy.GeneratePolicy<TResult>().WrapAsync(_retryPolicy.GeneratePolicy<TResult>());
        }

        public IAsyncPolicy Build()
        {
            return _notFoundPolicy.GeneratePolicy().WrapAsync(_retryPolicy.GeneratePolicy());
        }
    }
}