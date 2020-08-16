using System;
using CommonServiceLocator;
using SolrNet;
using SolrNet.Commands.Parameters;

namespace SolrEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup.Init<Lyrics>("http://localhost:8983/solr/test");
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Lyrics>>();

            QueryOptions query_options = new QueryOptions
            {
                Rows = 10, StartOrCursor = new StartOrCursor.Start(0)
            };
        
            // Construct the query
            SolrQuery query = new SolrQuery("singer:\"mile kitic\"");
            // Run a basic keyword search, filtering for questions only
            var posts = solr.Query(query, query_options);
            foreach (var post in posts)
            {
                Console.WriteLine(post.Lyric);
                if (post.Lyric.Contains("krec"))
                {
                    Console.WriteLine("sadrži");
                }
            }

            Console.WriteLine("Query done");

            Console.Read();
        }
    }
}
