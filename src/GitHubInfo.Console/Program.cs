using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;

namespace GitHubInfo
{
    class Program
    {
        static private IWindsorContainer container = new WindsorContainer();
        const int limitOfResults = 5;

        static void Main()
        {
            RegisterDependencies();
            var searchEngine = container.Resolve<ISearch>();

            Console.Write("Repository search criteria: ");
            var searchCriteria = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(searchCriteria))
            {
                Console.WriteLine("Criteria can not be empty");
                return;
            }

            var repositories = searchEngine.LookUpTopRepositories(searchCriteria, limitOfResults);
            foreach(var repository in repositories)
            {
                Console.WriteLine($"\n\nRepository:\nOwner name: {repository.OwnerName}");
                Console.WriteLine($"Repository name: {repository.RepositoryName}");
                Console.WriteLine($"Repository URL: {repository.RepositoryUrl}");
                Console.WriteLine($"Created date: {repository.CreatedDate}");
                Console.WriteLine($"Last push date: {repository.LastPushDate}");

                var commits = searchEngine.LookUpRecentCommits(repository.OwnerName, repository.RepositoryName, limitOfResults);
                foreach(var commit in commits)
                {
                    Console.WriteLine($"\nCommit:\nCommitter name: {commit.CommiterName}");
                    if (!string.IsNullOrEmpty(commit.Message))
                    {
                        Console.WriteLine($"Message: {commit.Message}");
                    }
                    Console.WriteLine($"SHA hash: {commit.ShaHash}");
                    Console.WriteLine($"Date: {commit.Date}");
                }
            }
        }

        static private void RegisterDependencies()
        {
            container.Register(
                         Component.For(typeof(ISearch))
                              .ImplementedBy(typeof(Search)));
            container.Register(
                         Component.For(typeof(IResultsParser))
                              .ImplementedBy(typeof(ResultsParser)));
        }
    }
}
