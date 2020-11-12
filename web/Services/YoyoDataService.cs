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

        public List<Athlete> Athletes { get; }

        public List<Shuttle> Shuttles { get; }

        public YoyoDataService(ILogger<YoyoDataService> logger, IRepository<Shuttle> shuttleRepo, IRepository<Athlete> athleteRepo)
        {
            _logger = logger;
            Shuttles = shuttleRepo?.Data.ToList();
            Athletes = athleteRepo?.Data.ToList();
        }

        public Shuttle CheckForMatchingShuttle(TimeSpan currentTime)
        {
            var matchedShuttle = Shuttles?.FirstOrDefault(s => s.StartTime == currentTime);
            return matchedShuttle;
        }

        /// <summary>
        /// Returns a Distinct combination of Speed & ShuttleNumber as a Tuple, sorted by shuttle first then Speed to show Yoyo test result as Speed-ShuttleNumber
        /// </summary>
        public List<(int SpeeLevel,int ShuttleNo)> GetShuttleResults()
        {
            return Shuttles?.Select(x => (x.SpeedLevel, x.ShuttleNo)).Distinct().OrderBy(y => y.ShuttleNo).ThenBy(z => z.SpeedLevel).ToList();
        }

    }
}