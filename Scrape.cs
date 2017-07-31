// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Scrape.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Scrape type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CsePlayer
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class Scrape
    {
        public string SubmitTime { get; set; }

        public string Owner { get; set; }

        public string Description { get; set; }

        public string Engine { get; set; }

        [XmlElement(ElementName = "EntitySetId")]
        public string QuerySetGuid { get; set; }

        [XmlElement(ElementName = "MiniSet")]
        public string QuerySetName { get; set; }

        [XmlElement(ElementName = "QueryCount")]
        public string SerpCount { get; set; }

        public string Market { get; set; }

        public string ScrapeDepth { get; set; }

        [XmlArray("Data")]
        [XmlArrayItem("Query")]
        public List<Serp> Serps { get; set; }
    }

    public class Serp
    {
        [XmlAttribute("id")]
        public string QueryGuid { get; set; }

        public string RawText { get; set; }

        public string Text { get; set; }

        public string Market { get; set; }

        [XmlArray("Results")]
        [XmlArrayItem("Result")]
        public List<Result> Results { get; set; }
    }

    public class Result
    {
        [XmlAttribute("AdjustedRank")]
        public string AdjustedRank { get; set; }

        public int Position { get; set; }

        public string Title { get; set; }

        [XmlElement("URL")]
        public string Url { get; set; }

        [XmlElement("DisplayURL")]
        public string DisplayUrl { get; set; }

        public string Snippet { get; set; }

        public string Language { get; set; }

        public string Tier { get; set; }
    }

    public class ScrapeExtension
    {
        public static Scrape GenerateScrapeFromCseResponse(CseResponse cseResponse)
        {
            return null;
        }
    }
}
