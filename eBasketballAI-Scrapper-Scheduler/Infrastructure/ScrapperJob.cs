using eBasketballScrapper.Application.Services;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Infrastructure
{
    [DisallowConcurrentExecution]
    public class ScrapperJob : IJob
    {
        private readonly ILogger<LoggingBackgroundJob> _logger;

        public ScrapperJob(ILogger<LoggingBackgroundJob> logger)
        {
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            HttpClient client = new HttpClient();
            var service = new MatchScrapperService();

            var page = 30;
            while (page >= 1)
            {
                using var contexto = new eBasketballDbContext();
                var url = $"https://betsapi.com/le/23105/Ebasketball-Battle--4x5mins/p.{page}";
                var response = service.CallUrl(url, client).Result;
                var games = service.ParseMatchTable(response);
                var select = from match in contexto.Matches
                             select match.Id;

                foreach (var match in games)
                {
                    var connection = new eBasketballDbContext();
                    if (!select.Contains(match.Id))
                    {
                        Console.WriteLine($"Adding Match {match.Url} to the Database");
                        connection.Matches.Add(match);
                    }
                    else
                    {
                        Console.WriteLine("Match already on the Database");
                        continue;
                    }

                    connection.SaveChanges();
                }
                contexto.SaveChanges();
                page--;
                Console.WriteLine($"Page: {page}");
            }
            return Task.CompletedTask;
        }
    }
}
