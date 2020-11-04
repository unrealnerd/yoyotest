using System;
using System.Timers;

namespace web.Services
{
    public class YoyoTimer
    {
        private Timer _timer;
        public event Action OnElapsed;
        
        public void SetTimer(double intervalMilliseconds, bool repeat)
        {
            _timer = new Timer(intervalMilliseconds);
            _timer.Elapsed += NotifyTimerElapsed;
            _timer.AutoReset = repeat;
            _timer.Enabled = true;
        }


        private void NotifyTimerElapsed(object source, ElapsedEventArgs e)
        {
            OnElapsed?.Invoke();
            if (!_timer.AutoReset)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
        }

    }
}