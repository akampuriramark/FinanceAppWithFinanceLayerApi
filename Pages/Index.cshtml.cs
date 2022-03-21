using FinanceAppWithFinanceLayerApi.Models;
using FinanceAppWithFinanceLayerApi.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceAppWithFinanceLayerApi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly INewsService _newsService;
        public FinanceNews? news;

        // Let's inject the News service 
        public IndexModel(INewsService newsService)
        {
            _newsService = newsService;
        }

        public void OnGet()
        {
            // on load get finance news, with an offset of 0
            news = GetFinanceNews(0);
        }

        public void OnGetLoadMoreNews(int offset)
        {
            // on load more news, get finance news, offsetting previously viewed articles
            news = GetFinanceNews(offset);
        }

        // method to get news from the NewsService
        public FinanceNews? GetFinanceNews(int offset)
        {
            return _newsService.GetFinanceNews(offset);
        }
    }
}