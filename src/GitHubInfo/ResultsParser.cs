using System.Collections.Generic;

namespace GitHubInfo
{
    public class ResultsParser : IResultsParser
    {
        public int ParseNumberOfResults(dynamic results)
        {
            return (int)results["total_count"];
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
            throw new System.NotImplementedException();
        }
    }
}
