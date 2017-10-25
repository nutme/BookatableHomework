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
    
        public RepositorySearchResult[] LookUpTopRepositories(string searchString, int numberOfResults)
        {
            var searchUrl = new Uri($"https://api.github.com/search/repositories?q={searchString}&sort=stars&order=desc");
            var results = MakeRequest(searchUrl);

            return resultsParser.ParseRepositorySearchResults(
                results,
                GetNumberOfResultsToTake(numberOfResults, resultsParser.ParseNumberOfResults(results)));
        }

        public CommitSearchResult[] LookUpRecentCommits(string owner, string repository, int numberOfCommits)
        {
            var searchUrl = new Uri($"https://api.github.com/repos/{owner}/{repository}/commits");
            var results = MakeRequest(searchUrl);

            return resultsParser.ParseCommitSearchResults(
                results,
                GetNumberOfResultsToTake(numberOfCommits, resultsParser.ParseNumberOfResults(results)));
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

        private dynamic MakeRequest(Uri searchUrl)
        {
            var request = CreateGetRequest(searchUrl);

            var results = GetResponse(request);
            return results;
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
