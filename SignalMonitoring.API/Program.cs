using CommonServiceLocator;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SolrEngine;
using SolrNet;

namespace SignalMonitoring.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SolrNet.Startup.Init<AnswerModel>("http://localhost:8983/solr/MuzickiPojam");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
