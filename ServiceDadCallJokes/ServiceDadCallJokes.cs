using Contract.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;

namespace ServiceDadCallJokes
{
    public class ServiceDadCallJokes : IServiceDadCallJokes
    {
        private HttpClient httpClient;
        private Dictionary<string, List<string>> jokes;

        public ServiceDadCallJokes() 
        {
            httpClient= new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Dictionary<string, List<string>>> GetJokesAsync(string searchTerm)
        {
            jokes = new Dictionary<string, List<string>>();
            var uri= new UriBuilder("https://icanhazdadjoke.com/search");
            uri.Query = $"term='{searchTerm}'";
            var result = await httpClient.GetAsync(uri.Uri);
            var result2 = result.Content.ReadAsStringAsync().Result;
            var result3 = JsonConvert.DeserializeObject(result2);
            var jObject = result3 as JObject;
            var resultJokes = jObject?["results"];
            AddingJokes(searchTerm, resultJokes);
            return jokes;
        }

        public async Task<string> GetRandomJokeAsync()
        {
            var uri = new UriBuilder("https://icanhazdadjoke.com");
            var result =await httpClient.GetAsync(uri.Uri);
            var result2 = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<Response>(result2);
            return response?.joke;
        }

        private void AddingJokes(string searchTerm, JToken? resultJokes)
        {
            foreach (var item in resultJokes?.Children())
            {
                if (jokes.Count < 30)
                {
                    var joke = item.SelectToken("joke").Value<string>();
                    var emphasizedString = joke.Replace(searchTerm, searchTerm.ToUpper());
                    var wordCount = WordsCount(emphasizedString);
                    if (wordCount < 10)
                    {
                        AddJoke("Short", emphasizedString);
                    }
                    else if (wordCount > 10 && wordCount < 20)
                    {
                        AddJoke("Medium", emphasizedString);
                    }
                    else
                    {
                        AddJoke("Long", emphasizedString);
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private void AddJoke(string key,string joke)
        {
            if (jokes.ContainsKey(key))
            {
                jokes[key].Add(joke);
            }
            else
            {
                jokes.Add(key, new List<string> { joke });
            }
        }

        private int WordsCount(string str)
        {
            char[] delimiters = new char[] { ' ', '\r', '\n' };
            var data = str.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            return data.Length;
        }

    }
}