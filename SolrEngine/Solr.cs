using System;
using CommonServiceLocator;
using SolrNet;
using SolrNet.Commands.Parameters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolrEngine
{
    public class Solr
    {
        private ISolrOperations<AnswerModel> solr;
		private QueryOptions query_options;

		public Solr()
		{
            solr = ServiceLocator.Current.GetInstance<ISolrOperations<AnswerModel>>();
            query_options = new QueryOptions
            {
                Rows = 10,
                StartOrCursor = new StartOrCursor.Start(0)
            };
        }
        public async Task<string> ValidateAnswer(AnswerModel answer, string term)
        {
            string query = term.Prerequisite().And();

            query += GenerateQuery(answer);
            
            try
            {
                var posts = await solr.QueryAsync(new SolrQuery(query), query_options); 

                return posts.First().Id;
            }
            catch (Exception)
			{
                return string.Empty;
            }

        }

        private static string GenerateQuery(AnswerModel answer)
        {
            if (!string.IsNullOrEmpty(answer.Lyric))
            {
                return $"lyric:\"{answer.Lyric}\"";
            }

            var singer = $"singer:\"{answer.Singer}\"";
            var title = $"title:\"{answer.Title}\"";
            return singer.And(title);
        }

    }

	public static class MyClass
	{
        public static string And(this string s, string and = "")
        {
            return s + " && " + and;
        }

        public static string Prerequisite(this string s)
        {
            return $"lyric:\"{s}\"";
        }

	}
}
