using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace GitHubInfo
{
    public class Search : ISearch
    {
        private readonly IResultsParser resultsParser;

        public Search(IResultsParser resultsParser)
        {
            this.resultsParser = resultsParser;
        }


        public SearchResult[] LookUpTopMatches(string searchString, int numberOfResults)
        {
            var searchUrl = new Uri($"https://api.github.com/search/repositories?q={searchString}&sort=stars&order=desc");
            var request = CreateGetRequest(searchUrl);

            var results = GetResponse(request);

            return resultsParser.ParseRepositorySearchResults(
                results,
                GetNumberOfResultsToTake(numberOfResults, resultsParser.ParseNumberOfResults(results)));
        }

        private HttpWebRequest CreateGetRequest(Uri uri)
        {
            var request = WebRequest.Create(uri) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json";
            request.UserAgent = "BookatableHomework";

            return request;
        }

        private dynamic GetResponse(HttpWebRequest request)
        {
            using (HttpWebResponse resp = request.GetResponse() as HttpWebResponse)
            {
                var reader = new StreamReader(resp.GetResponseStream());
                var jsonResults = reader.ReadToEnd();
                if (string.IsNullOrEmpty(jsonResults))
                {
                    throw new Exception("Empty result from GitHub.com");
                }

                var results = JsonConvert.DeserializeObject<dynamic>(jsonResults);
                return results;
            }
        }

        private int GetNumberOfResultsToTake(int numberAsked, int numberAvailable)
        {
            if (numberAsked < numberAvailable)
            {
                return numberAsked;
            }

            return numberAvailable;
        }
    }
}
