// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the CseResponse type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CsePlayer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;

    using Newtonsoft.Json;

    public class CsePlayGround
    {
        public static void GetCseReponse()
        {
            string domainUrl = @"https://www.googleapis.com/customsearch/v1?{0}";
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                string query = "Lei Sun";

                Dictionary<string, string> urlParams = new Dictionary<string, string>
                {
                    { "key", @"AIzaSyB_EQNNiy-B3c1Xc6QsX7dzV7ioRYjr1P4" },          // myApiPassword
                    { "cx", @"013503392527588303266:5a9ftmsoybg" },                 // cseId
                    { "q", query }                                                  // query
                };

                string paramString = string.Join("&", urlParams.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value)));
                string apiUrl = string.Format(domainUrl, paramString);

                HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                File.WriteAllText(@"C:\Users\leis\Desktop\cseret.txt", result);
            }

            string responseContent = File.ReadAllText(@"C:\Users\leis\Desktop\cseret.txt");
            var dd = JsonConvert.DeserializeObject<CseResponse>(responseContent);
            Console.WriteLine(dd);
        }
    }

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
        }
    }
}
