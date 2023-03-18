using System.Timers;

namespace WhiskyWebscraper.Jobs;

public class PeriodicExecutor : IDisposable
{
    public event EventHandler<JobExecutedEventArgs> JobExecuted;

    void OnJobExecuted()
    {
        JobExecuted?.Invoke(this, new JobExecutedEventArgs());
    }

    System.Timers.Timer _timer;
    bool _running;
    public int Count { get; set; }

    public void StartExecuting()
    {
        if (!_running)
        {
            _timer = new System.Timers.Timer();
            _timer.Interval = TimeSpan.FromSeconds(5).TotalMilliseconds;
            //_timer.Interval = TimeSpan.FromMinutes(1).TotalMilliseconds;
            _timer.Elapsed += HandleTimer;
            _timer.AutoReset = true;
            _timer.Enabled = true;

            _running = true;
        }
    }

    private void HandleTimer(object source, ElapsedEventArgs e)
    {
        Count++;

        OnJobExecuted();
    }

    public void Dispose()
    {
        if (_running) 
        {
            _timer.Stop();
            _running = false;
            Count = 0;
        }
    }
}
