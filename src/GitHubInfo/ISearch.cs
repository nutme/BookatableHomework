namespace GitHubInfo
{
    public interface ISearch
    {
        RepositorySearchResult[] LookUpTopRepositories(string searchString, int numberOfResults);
        CommitSearchResult[] LookUpRecentCommits(string owner, string rep, int numberOfCommits);
    }
}
