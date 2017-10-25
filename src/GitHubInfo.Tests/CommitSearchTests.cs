using NUnit.Framework;

namespace GitHubInfo.Tests
{
    [TestFixture]
    public class CommitSearchTests
    {
        [Test]
        public void SearchResultsAreNotEmpty()
        {
            var search = new Search(new ResultsParser());
            var results = search.LookUpRecentCommits("admin", "cable", 5);

            foreach (var result in results)
            {
                Assert.That(result.CommiterName, Is.Not.Empty);
                Assert.That(result.Date, Is.Not.Empty);
                Assert.That(result.ShaHash, Is.Not.Empty);
            }
        }
    }
}
