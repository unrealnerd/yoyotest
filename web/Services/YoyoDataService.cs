using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using web.Data;
using web.Models;

namespace web.Services
{
    public class YoyoDataService
    {
        private readonly ILogger<YoyoDataService> _logger;
        private readonly YoyoTimer _yoyoTimer;
        private TimeSpan _elapsedTime;
        private readonly IList<Shuttle> _shuttles;

        public delegate void ShuttleChangedHandler(Shuttle shuttle);

        public event ShuttleChangedHandler OnShuttleChanged;

        public YoyoDataService(ILogger<YoyoDataService> logger, YoyoTimer timer, Repository<Shuttle> shuttleRepo)
        {
            _logger = logger;
            _yoyoTimer = timer;
            _shuttles = shuttleRepo.Data;
        }

        public void Start()
        {
            NotifyShuttleChanged(_shuttles[0]);
            _yoyoTimer.OnElapsed += () =>
            {
                _elapsedTime = _elapsedTime.Add(new TimeSpan(0, 0, 1));

                _logger.LogDebug($"Elapsed Time: {_elapsedTime:mm\\:ss}");

                var matchedShuttle = _shuttles.FirstOrDefault(s => s.StartTime == _elapsedTime.ToString(@"mm\:ss"));
                if (matchedShuttle != null)
                {
                    NotifyShuttleChanged(matchedShuttle);
                    _logger.LogInformation($"Matched Shuttle: {JsonSerializer.Serialize(matchedShuttle)}");
                }
            };
        }

        private void NotifyShuttleChanged(Shuttle s)
        {
            OnShuttleChanged?.Invoke(s);
        }


    }
}