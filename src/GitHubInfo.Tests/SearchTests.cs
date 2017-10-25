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
            var results = Search("good");
            Assert.That(results.Count(), Is.EqualTo(5));
        }

        [Test]
        public void SearchByStringWithSpacesReturnsResults()
        {
            var results = Search("twin peaks");
            Assert.That(results.Count(), Is.EqualTo(5));
        }

        [Test]
        public void SearchResultsAreNotEmpty()
        {
            var results = Search("cable");
            foreach(var result in results)
            {
                Assert.That(result.RepositoryName, Is.Not.Empty);
                Assert.That(result.OwnerName, Is.Not.Empty);
                Assert.That(result.RepositoryName, Is.Not.Empty);
                Assert.That(result.CreatedDate, Is.Not.Empty);
                Assert.That(result.LastPushDate, Is.Not.Empty);
            }
        }

        [Test]
        public void SearchForNoMatchesWillHaveNoResults()
        {
            var results = Search("kaelfkj ekawefl kjawefl jbelwfjha bef");
            Assert.That(results.Count(), Is.EqualTo(0));
        }

        private SearchResult[] Search(string criteria)
        {
            var search = new Search();
            var results = search.LookUpTopMatches(criteria, 5);
            return results;
        }
    }
}
