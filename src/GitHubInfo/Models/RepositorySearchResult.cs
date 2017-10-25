namespace GitHubInfo
{
    public class RepositorySearchResult
    {
        public string OwnerName { get; private set; }
        public string RepositoryName { get; private set; }
        public string RepositoryUrl { get; private set; }
        public string CreatedDate { get; private set; }
        public string LastPushDate { get; private set; }

        public RepositorySearchResult(string repositoryName, string ownerName, string repositoryUrl, string createdDate, string lastPushDate)
        {
            RepositoryName = repositoryName;
            OwnerName = ownerName;
            RepositoryUrl = repositoryUrl;
            CreatedDate = createdDate;
            LastPushDate = lastPushDate;
        }
    }
}
