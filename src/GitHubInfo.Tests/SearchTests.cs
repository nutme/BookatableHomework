using NUnit.Framework;
using System.Linq;

namespace GitHubInfo.Tests
{
    [TestFixture]
    public class SearchTests
    {
        [Test]
        public void SearchByStringReturnsTop5Results()
        {
            var testString = "repo";
            var search = new Search();

            var results = search.LookUpTopMatches(testString, 5);
            Assert.That(results.Count(), Is.EqualTo(5));
        }
    }
}
