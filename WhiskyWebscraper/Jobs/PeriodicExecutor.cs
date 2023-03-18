using System.Timers;

namespace WhiskyWebscraper.Jobs;

public class PeriodicExecutor : IDisposable
{
    public event EventHandler<JobExecutedEventArgs> JobExecuted;

    void OnJobExecuted()
    {
        JobExecuted?.Invoke(this, new JobExecutedEventArgs());
    }

    PeriodicTimer _timer;
    bool _running;

    public async void StartExecuting()
    {
        if (!_running)
        {
            _timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
            while (await _timer.WaitForNextTickAsync()) 
            {
                await HandleTimer();
            }

            _running = true;
        }
    }

    private async Task HandleTimer()
    {
        var content = await HeadlessBrowser.GetPageContent("https://whiskyhaus.de/");
        if (content != null)
        {
            await WebPageAnalyzer.Analyze(content);
        }

        OnJobExecuted();
    }

    public void Dispose()
    {
        if (_running) 
        {
            _timer.Dispose();
            _running = false;
        }
    }
}
