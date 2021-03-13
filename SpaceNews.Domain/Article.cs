using System;
using System.Collections.Generic;

namespace SpaceNews.Domain
{
    public sealed class Article
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public Uri Url { get; set; }
        public Uri ImageUrl { get; set; }
        public string NewsSite { get; set; }
        public string Summary { get; set; }
        public DateTimeOffset PublishedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public bool Featured { get; set; }
        public List<Event> Launches { get; set; }
        public List<Event> Events { get; set; }
    }
}