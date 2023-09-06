using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    public static string MarketWatchParentNode = "//div[@class='component component--layout layout--D4']";
    public static string MarketWatchChildNode = ".//div[@data-guid][@class='element element--article    MarketWatchcom' or @class='element element--article    BarronsOnline']";

    static async Task Main(string[] args)
    {
        try
        {
            var parentNode = "";
            var childNode = "";
            // Initialize HttpClient
            HttpClient httpClient = new HttpClient();
            string url = "https://www.marketwatch.com/markets";

            string url2 = "https://www.businesstimes.com.sg/breaking-news";
            string html2 = await httpClient.GetStringAsync(url2);

            switch (url)
            {
                case "https://www.marketwatch.com/markets":
                    LoadHtml(html2, MarketWatchParentNode, MarketWatchChildNode);
                    break;
                case "https://www.businesstimes.com.sg/breaking-news":
                    break;
            }


            // Send an HTTP GET request to the MarketWatch Markets page
            string html = await httpClient.GetStringAsync(url);

            // Load HTML content into HtmlAgilityPack's HtmlDocument
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Select the container div with class 'component--layout layout--D4'
            //var containerNode = doc.DocumentNode.SelectSingleNode("//div[@class='component component--layout layout--D4']");

            //if (containerNode != null)
            //{
            //    // Select all article nodes within the container using CSS selectors
            //    var articleNodes = containerNode.SelectNodes(".//div[@data-guid][@class='element element--article    MarketWatchcom' or @class='element element--article    BarronsOnline']");

            //    if (articleNodes != null)
            //    {
            //        foreach (var articleNode in articleNodes)
            //        {
            //            // Extract data from each article node
            //            string headline = articleNode.SelectSingleNode(".//h3/a")?.InnerText.Trim();
            //            string link = articleNode.SelectSingleNode(".//h3/a")?.GetAttributeValue("href", "");
            //            string summary = articleNode.SelectSingleNode(".//p[@class='article__summary']")?.InnerText.Trim();
            //            string imageSrc = articleNode.SelectSingleNode(".//img[@class='lazyload']")?.GetAttributeValue("data-srcset", "");


            //            // Print or process the extracted data
            //            Console.WriteLine($"Headline: {headline}");
            //            Console.WriteLine($"Link: {link}");
            //            Console.WriteLine($"Summary: {summary}");
            //            Console.WriteLine($"Image Source: {imageSrc}");
            //            Console.WriteLine();
            //        }
            //    }
            //}
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static void LoadHtml(string html, string parentNode, string childNode)
    {
        // Load HTML content into HtmlAgilityPack's HtmlDocument
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(html);

        // Select the container div with class 'component--layout layout--D4'
        var containerNode = doc.DocumentNode.SelectSingleNode(parentNode);

        if (containerNode != null)
        {
            // Select all article nodes within the container using CSS selectors
            var articleNodes = containerNode.SelectNodes(childNode);

            if (articleNodes != null)
            {
                foreach (var articleNode in articleNodes)
                {
                    // Extract data from each article node
                    string headline = articleNode.SelectSingleNode(".//h3/a")?.InnerText.Trim();
                    string link = articleNode.SelectSingleNode(".//h3/a")?.GetAttributeValue("href", "");
                    string summary = articleNode.SelectSingleNode(".//p[@class='article__summary']")?.InnerText.Trim();
                    string imageSrc = articleNode.SelectSingleNode(".//img[@class='lazyload']")?.GetAttributeValue("data-srcset", "");


                    // Print or process the extracted data
                    Console.WriteLine($"Headline: {headline}");
                    Console.WriteLine($"Link: {link}");
                    Console.WriteLine($"Summary: {summary}");
                    Console.WriteLine($"Image Source: {imageSrc}");
                    Console.WriteLine();
                }
            }
        }
    }

    static void SaveToNotepad(string content, string filePath)
    {
        // Save content to a Notepad file
        File.AppendAllText(filePath, content);
    }
}
