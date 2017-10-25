namespace GitHubInfo
{
    public class CommitSearchResult
    {
        public string CommiterName { get; private set; }
        public string Message { get; private set; }
        public string ShaHash { get; private set; }
        public string Date { get; private set; }

        public CommitSearchResult(string commiterName, string message, string shaHash, string date)
        {
            CommiterName = commiterName;
            Message = message;
            ShaHash = shaHash;
            Date = date;
        }
    }
}
