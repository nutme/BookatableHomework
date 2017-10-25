namespace GitHubInfo
{
    public interface IResultsParser
    {
        int ParseNumberOfResults(dynamic results);
        SearchResult[] ParseRepositorySearchResults(dynamic results, int numberOfResultsToTake);
    }
}
