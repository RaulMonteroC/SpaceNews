using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SpaceNews.Foundation.Http.Policies;
using SpaceNews.Foundation.Attributes;
using Refit;

namespace SpaceNews.Foundation.Http
{
    public sealed class ApiService<TApi> : IApiService<TApi>
    {
        public ApiService() => _policyBuilder = new PolicyBuilder();

        public Task Call(Func<TApi, Task> apiCall) =>
            _policyBuilder.Build()
                          .ExecuteAsync(async () => await apiCall(LoadApiDefinition()));

        public Task<TResult> Call<TResult>(Func<TApi, Task<TResult>> apiCall) =>
            _policyBuilder.Build<TResult>()
                          .ExecuteAsync(async () => await apiCall(LoadApiDefinition()));

        private static TApi LoadApiDefinition() =>
            RestService.For<TApi>(
                new HttpClient(new LoggedHttpClientHandler())
                {
                    BaseAddress = new Uri(GetUrl())
                },
                new RefitSettings
                {
                    ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    })
                });

        private static string GetUrl() => ((UrlAttribute) Attribute.GetCustomAttribute(typeof(TApi), typeof(UrlAttribute)))?.Url;

        private readonly PolicyBuilder _policyBuilder;
    }
}
