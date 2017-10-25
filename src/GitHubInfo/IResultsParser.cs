namespace GitHubInfo
{
    public interface IResultsParser
    {
        int ParseNumberOfResults(dynamic results);
        RepositorySearchResult[] ParseRepositorySearchResults(dynamic results, int numberOfResultsToTake);
        CommitSearchResult[] ParseCommitSearchResults(dynamic results, int numberOfResultsToTake);
    }
}
