namespace FinanceAppWithFinanceLayerApi.Models
{
    public class FinanceNews
    {
        // pagination details
        public Pagination? Pagination { get; set; }
        // th data will be a list of news articles
        public List<NewsArticle>? Data { get; set; }
    }
}
