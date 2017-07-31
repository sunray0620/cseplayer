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
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

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
        private const string CseApiKey = @"AIzaSyB_EQNNiy-B3c1Xc6QsX7dzV7ioRYjr1P4";

        private const string CseId = @"013503392527588303266:5a9ftmsoybg";

        public static Scrape GenerateScrape(List<Query> queries)
        {
            Scrape scrape = new Scrape
            {
                Serps = new List<Serp>(),
                SubmitTime = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                Owner = "Lei Sun",
                Description = "CSE Test",
                QuerySetGuid = "Custom Query Set",
                QuerySetName = "Custom Query Set",
                Market = "un-UN",
                ScrapeDepth = "10"
            };

            foreach (var q in queries)
            {
                string cseResponseString = GetCseReponse(q);
                BackupCseResponse(q, cseResponseString);

                CseResponse cseResponse = JsonConvert.DeserializeObject<CseResponse>(cseResponseString);
                Serp newSerp = GenerateSerp(q, cseResponse);
                scrape.Serps.Add(newSerp);
            }

            return scrape;
        }

        public static Serp GenerateSerp(Query query, CseResponse cseResponse)
        {
            Serp serp = new Serp
            {
                QueryGuid = query.QueryId,
                RawText = query.QueryText,
                Text = query.QueryText,
                Market = query.Market,
                Results = new List<Result>()
            };

            int pos = 1;
            foreach (CseSearchResult cseret in cseResponse.CseSearchResults)
            {
                Result newRet = GenerateResult(cseret, pos);
                serp.Results.Add(newRet);
                pos += 1;
            }

            return serp;
        }

        public static Result GenerateResult(CseSearchResult cseSearchResult, int position)
        {
            Result ret = new Result
            {
                Title = cseSearchResult.Title,
                Url = cseSearchResult.Link,
                DisplayUrl = cseSearchResult.DisplayLink,
                Snippet = cseSearchResult.Snippet,
                Position = position
            };

            return ret;
        }

        public static string GetCseReponse(Query query)
        {
            string domainUrl = @"https://www.googleapis.com/customsearch/v1?{0}";
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                Dictionary<string, string> urlParams = new Dictionary<string, string>
                {
                    { "key", CseApiKey },               // myApiPassword
                    { "cx", CseId },                    // cseId
                    { "q", query.QueryText }            // query
                };

                string paramString = string.Join("&", urlParams.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value)));
                string apiUrl = string.Format(domainUrl, paramString);

                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
        }

        public static void BackupCseResponse(Query query, string cseResponseString)
        {
            const string ConnStr = "ddd";

            using (CseDbContext db = new CseDbContext(ConnStr))
            {
                CseDbRecord dbRecord = new CseDbRecord
                {
                    QueryId = query.QueryId,
                    CreatedTime = DateTime.UtcNow,
                    QueryText = query.QueryText,
                    Market = query.Market,
                    CseResponse = cseResponseString,
                };

                db.CseDbRecords.Add(dbRecord);
                db.SaveChanges();
            }
        }
    }
}
