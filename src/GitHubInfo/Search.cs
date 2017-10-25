using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace GitHubInfo
{
    public class Search : ISearch
    {
        public SearchResult[] LookUpTopMatches(string searchString, int numberOfResults)
        {
            var searchUrl = new Uri($"https://api.github.com/search/repositories?q={searchString}&sort=stars&order=desc");
            string outpuJson = string.Empty;

            var req = WebRequest.Create(searchUrl) as HttpWebRequest;
            req.Method = "GET";
            req.ContentType = "application/json";
            req.UserAgent = "BookatableHomework";

            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                var reader = new StreamReader(resp.GetResponseStream());
                var jsonResults = reader.ReadToEnd();
                if (string.IsNullOrEmpty(jsonResults))
                {
                    throw new Exception("Empty result from GitHub.com");
                }

                var results = JsonConvert.DeserializeObject<dynamic>(jsonResults);

                var totalResultsCount = (int)results["total_count"];
                if (numberOfResults > totalResultsCount)
                {
                    numberOfResults = totalResultsCount;
                }

                var parsedResults = new List<SearchResult>();
                for (var index = 0; index < numberOfResults; index++)
                {
                    var result = results["items"][index];
                    parsedResults.Add(new SearchResult(
                        result["name"].ToString(),
                        result["owner"]["login"].ToString(),
                        result["html_url"].ToString(),
                        result["created_at"].ToString(),
                        result["pushed_at"].ToString()
                    ));
                }

                return parsedResults.ToArray();
            }
        }
    }
}
