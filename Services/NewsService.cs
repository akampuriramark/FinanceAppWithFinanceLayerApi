using FinanceAppWithFinanceLayerApi.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace FinanceAppWithFinanceLayerApi.Services
{
    // since this is going to be used as a service, we define an interface INewsService
    // that will be implemented by NewsService. 
    public interface INewsService
    {
        FinanceNews? GetFinanceNews(int offset);
    }
    public class NewsService : INewsService
    {
        private readonly IConfiguration _configuration;
        // inject the configuration object into the NesService class to be able to read configurations
        // like the api key and the api url.
        public NewsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // method to GET news from the api
        public FinanceNews? GetFinanceNews(int offset)
        {
            // let's define the parameters wtith which to call our api.
            var parameters = new Dictionary<string, string>();
            // add our access key. We are going to copy this from our dashoboard.
            parameters.Add("access_key", _configuration.GetValue<string>("ACCESS_KEY"));
            // let's add a parameter t get news for today
            parameters.Add("date", "today");
            // let's also add a parameter to sort the results by latest published date first
            parameters.Add("sort", "desc");
            // let's also add a parameter to limit our article results to 10 
            parameters.Add("limit", "15");
            // let's also add a parameter for offset to load more 
            parameters.Add("offset", $"{offset}");


            // we now need to convert the dictionary of parameters into a string
            var stringParameters = $"?{ToString(parameters)}";

            // initialise an HTTP client that we'll use to call the api 
            using (var client = new HttpClient())
            {
                // set client url to the api endpoint URL
                client.BaseAddress = new Uri(_configuration.GetValue<string>("API_URL"));
                client.DefaultRequestHeaders.Accept.Clear();
                // set the response type for our api to JSON
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // make a call to the api with parameters (access_key, date and limit)
                HttpResponseMessage response = client.GetAsync(stringParameters).Result;

                // Check that the response is a successful reaponse from the api
                if (response.IsSuccessStatusCode)
                {
                    var dataResult = response.Content.ReadAsStringAsync().Result;
                    // to be able to desearilaize the string JSON into our FinanceNews object, we use
                    // a powerful library Newtonsoft. This library automatically parses the JSON string into
                    // FinanceNews object.
                    // To use do this, we use a shortcut Ctrl + . and get an option to install the library.
                    return JsonConvert.DeserializeObject<FinanceNews>(dataResult);
                }
                else
                {
                    return new FinanceNews()
                    {
                        // return an empty list of news articles
                        Data = new List<NewsArticle>(),
                        // return default pagination details
                        Pagination = new Pagination()
                    };
                }
            }

        }

        // Method to generate key value pair parameters into url endoced parameters
        public static string ToString(Dictionary<string, string> parameters)
        {
            return string.Join("&", parameters.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value)));
        }
    }
}
