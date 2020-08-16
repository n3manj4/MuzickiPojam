using System;
using System.Collections.Generic;
using System.Text;
using SolrNet.Attributes;

namespace SolrEngine
{
    internal class Lyrics
    {
        [SolrUniqueKey("id")]
        public string Id { get; set; }

        [SolrField("singer")]
        public string Singer { get; set; }
        [SolrField("title")]
        public string Title { get; set; }
        [SolrField("lyric")]
        public string Lyric { get; set; }
    }
}
