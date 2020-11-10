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
        private readonly List<Shuttle> _shuttles;
        private readonly List<Athlete> _athletes;

        public delegate void ShuttleChangedHandler(Shuttle shuttle);

        public event ShuttleChangedHandler OnShuttleChanged;

        public YoyoDataService(ILogger<YoyoDataService> logger, Repository<Shuttle> shuttleRepo, Repository<Athlete> athleteRepo)
        {
            _logger = logger;
            _shuttles = shuttleRepo.Data.ToList();
            _athletes = athleteRepo.Data.ToList();
        }

        public Shuttle CheckForMatchingShuttle(TimeSpan currentTime)
        {
            var matchedShuttle = _shuttles.FirstOrDefault(s => s.StartTime == currentTime);
            return matchedShuttle;
        }

        public List<Athlete> GetAthletes()
        {
            return _athletes;
        }

        public List<Shuttle> GetShuttles()
        {
            return _shuttles;
        }

    }
}