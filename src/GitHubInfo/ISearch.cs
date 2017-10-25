namespace GitHubInfo
{
    public interface ISearch
    {
        SearchResult[] LookUpTopMatches(string searchString, int numberOfResults);
    }
}
