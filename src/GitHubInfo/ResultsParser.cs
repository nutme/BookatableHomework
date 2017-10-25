using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace GitHubInfo
{
    public class ResultsParser : IResultsParser
    {
        public int ParseNumberOfResults(dynamic results)
        {
            return ((JArray)results).Count;
        }

        public RepositorySearchResult[] ParseRepositorySearchResults(dynamic results, int numberOfResultsToTake)
        {
            var parsedResults = new List<RepositorySearchResult>();
            for (var index = 0; index < numberOfResultsToTake; index++)
            {
                var result = results["items"][index];
                parsedResults.Add(new RepositorySearchResult(
                    result["name"].ToString(),
                    result["owner"]["login"].ToString(),
                    result["html_url"].ToString(),
                    result["created_at"].ToString(),
                    result["pushed_at"].ToString()
                ));
            }

            return parsedResults.ToArray();
        }
        
        public CommitSearchResult[] ParseCommitSearchResults(dynamic results, int numberOfResultsToTake)
        {
            var parsedResults = new List<CommitSearchResult>();
            for (var index = 0; index < numberOfResultsToTake; index++)
            {
                var result = results[index];
                parsedResults.Add(new CommitSearchResult(
                    result["commit"]["committer"]["name"].ToString(),
                    result["commit"]["message"].ToString(),
                    result["sha"].ToString(),
                    result["commit"]["committer"]["date"].ToString()
                ));
            }

            return parsedResults.ToArray();
        }
    }
}
