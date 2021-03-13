using System;

namespace SpaceNews.Foundation.Attributes
{
    [AttributeUsage(AttributeTargets.Interface)]
    public sealed class UrlAttribute : Attribute
    {
        public UrlAttribute(string url)
        {
            Url = url;
        }

        public string Url { get; }
    }
}