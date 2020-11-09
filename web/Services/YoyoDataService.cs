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
        private readonly IList<Shuttle> _shuttles;

        public delegate void ShuttleChangedHandler(Shuttle shuttle);

        public event ShuttleChangedHandler OnShuttleChanged;

        public YoyoDataService(ILogger<YoyoDataService> logger, Repository<Shuttle> shuttleRepo)
        {
            _logger = logger;
            _shuttles = shuttleRepo.Data;
        }

        public Shuttle CheckForMatchingShuttle(TimeSpan currentTime)
        {
            var matchedShuttle = _shuttles.FirstOrDefault(s => s.StartTime == currentTime);
            return matchedShuttle;
        }

    }
}