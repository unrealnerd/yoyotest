using System;
using System.Timers;
using Microsoft.Extensions.Logging;

namespace web.Services
{
    public class YoyoTimer : IDisposable
    {
        private Timer _timer;
        private readonly ILogger<YoyoTimer> _logger;

        public event Action OnElapsed;
        public YoyoTimer(ILogger<YoyoTimer> logger)
        {

            _logger = logger;
        }

        public void Start()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += NotifyTimerElapsed;
            _timer.Start();

            _logger.LogDebug("Timer Started!");
        }

        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();

                _logger.LogDebug("Timer Stopped!");
            }
        }

        private void NotifyTimerElapsed(object source, ElapsedEventArgs e)
        {
            OnElapsed?.Invoke();
        }

        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }
        }
    }
}