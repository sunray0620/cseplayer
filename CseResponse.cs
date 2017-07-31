// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CseResponse.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the CseResponse type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace CsePlayer
{
    using Newtonsoft.Json;

    public class CseResponse
    {
        [JsonProperty(PropertyName = "items")]
        public List<CseSearchResult> CseSearchResults { get; set; }
    }

    public class CseSearchResult
    {
        [JsonProperty(PropertyName = "kind")]
        public string ResultKind { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "htmlTitle")]
        public string HtmlTitle { get; set; }

        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }

        [JsonProperty(PropertyName = "displayLink")]
        public string DisplayLink { get; set; }

        [JsonProperty(PropertyName = "snippet")]
        public string Snippet { get; set; }

        [JsonProperty(PropertyName = "htmlSnippet")]
        public string HtmlSnippet { get; set; }
    }
}
