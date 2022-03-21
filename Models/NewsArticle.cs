using Newtonsoft.Json;

namespace FinanceAppWithFinanceLayerApi.Models
{
    public class NewsArticle
    {

        // the title of the news article
        [JsonProperty("title")]
        public string? Title { get; set; }
        // the url to th full news article
        [JsonProperty("url")]
        public string? Url { get; set; }
        // a summary of what the news article has
        [JsonProperty("description")]
        public string? Description { get; set; }
        // the site where the news article was published
        [JsonProperty("source")]
        public string? Source { get; set; }
        // news article tickers
        [JsonProperty("tickers")]
        public List<string>? Tickers { get; set; }
        // news article tags
        [JsonProperty("tags")]
        public List<string>? Tags { get; set; }
        // date of publish of the news article.
        [JsonProperty("published_at")]
        public DateTime PublishedAt { get; set; }
    }
}
