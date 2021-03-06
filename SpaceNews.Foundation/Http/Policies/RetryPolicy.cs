using System;
using System.Diagnostics;
using Refit;
using Polly;

namespace SpaceNews.Foundation.Http.Policies
{
    internal class RetryPolicy : IPolicy
    {
        public IAsyncPolicy GeneratePolicy() =>
            Policy.Handle<ApiException>().WaitAndRetryAsync(
                NumberOfRetries,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2.5, retryAttempt)),
                (exception, timeSpan, attempt, context) =>
                {
                    LogRetryAttempt(attempt, NumberOfRetries, exception);
                });

        public IAsyncPolicy<TResult> GeneratePolicy<TResult>() =>
            Policy<TResult>.Handle<ApiException>().WaitAndRetryAsync(
                NumberOfRetries,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2.5, retryAttempt)),
                (exception, timeSpan, attempt, context) =>
                {
                    LogRetryAttempt(attempt, NumberOfRetries, exception.Exception);
                });

        private void LogRetryAttempt(int attempt, int numberOfRetries, Exception exception)
        {
            Debug.WriteLine("=================== Exception ==========================");
            Debug.WriteLine($"Retry attempt {attempt} of {numberOfRetries}");
            Debug.WriteLine(string.Empty);
            Debug.WriteLine(($"Exception: {exception}."));
            Debug.WriteLine("========================================================");
        }

        private const int NumberOfRetries = 2;
    }
}