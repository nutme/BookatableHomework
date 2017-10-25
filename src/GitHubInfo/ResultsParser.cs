using System.Collections.Generic;

namespace GitHubInfo
{
    public class ResultsParser : IResultsParser
    {
        public int ParseNumberOfResults(dynamic results)
        {
            return (int)results["total_count"];
        }

        public SearchResult[] ParseRepositorySearchResults(dynamic results, int numberOfResultsToTake)
        {
            var parsedResults = new List<SearchResult>();
            for (var index = 0; index < numberOfResultsToTake; index++)
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
