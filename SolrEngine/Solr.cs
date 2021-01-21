using System;
using CommonServiceLocator;
using SolrNet;
using SolrNet.Commands.Parameters;
using System.Collections.Generic;
using System.Linq;

namespace SolrEngine
{
    public static class Solr
    {
        public static bool ValidateAnswer(AnswerModel answer, string term)
        {
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<AnswerModel>>();

            QueryOptions query_options = new QueryOptions
            {
                Rows = 10,
                StartOrCursor = new StartOrCursor.Start(0)
            };
            string query = term.Prerequisite().And();

            query += GenerateQuery(answer);
            
            // Construct the query
            SolrQuery solrQuery = new SolrQuery(query);
            // Run a basic keyword search, filtering for questions only
            try
            {
                var posts = solr.Query(solrQuery, query_options); 

                return posts.Any();

            }
            catch (Exception e)
            {
                return false;
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

        private static string And(this string s, string and = "")
        {
            return s + " && " + and;
        }

        private static string Prerequisite(this string s)
        {
            return $"lyric:\"{s}\"";
        }
    }
}
