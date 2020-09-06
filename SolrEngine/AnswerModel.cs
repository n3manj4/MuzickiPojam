using SolrNet.Attributes;

namespace SolrEngine
{
    public class AnswerModel
    {
        [SolrUniqueKey("id")]
        public string Id { get; set; }

        [SolrField("singer")]
        public string Singer { get; set; }
        [SolrField("title")]
        public string Title { get; set; }
        [SolrField("lyric")]
        public string Lyric { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}
