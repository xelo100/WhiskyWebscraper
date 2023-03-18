using AngleSharp;
using AngleSharp.Dom;

namespace WhiskyWebscraper.Jobs;

public static class WebPageAnalyzer
{
    private static List<string?> previousNewArrivals = new();

    public static async Task Analyze(string content)
    {
        var context = BrowsingContext.New(Configuration.Default);
        var document = await context.OpenAsync(req => req.Content(content));

        var newElements = document.QuerySelectorAll("div.flickity-cell");
        var newArrivals = new List<string?>();

        foreach ( var element in newElements )
        {
            var elementName = element.QuerySelector("h2")?.Text();
            newArrivals.Add(elementName);

            if (!previousNewArrivals.Contains(elementName))
            {

            }
        }

        previousNewArrivals = newArrivals;
    }
}
