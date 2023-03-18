
using Microsoft.Playwright;

namespace WhiskyWebscraper.Jobs;

public static class HeadlessBrowser
{
    public static async Task<string> GetPageContent(string url)
    {
        //using var playwright = await Playwright.CreateAsync();
        //await using var browser = await playwright.Chromium.LaunchAsync();

        //var page = await browser.NewPageAsync();
        //await page.GotoAsync(url);

        //return await page.ContentAsync();

        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }    
}
