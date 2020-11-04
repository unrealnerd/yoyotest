using System;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using web.Models;

namespace web.Services
{
    public class YoyoDataService
    {
        private IWebHostEnvironment _hostingEnvironment;
        private YoyoTimer _yoyoTimer;
        private TimeSpan _elapsedTime;
        private Shuttle[] _shuttles;

        public delegate void ShuttleChangedHandler(Shuttle shuttle);

        public event ShuttleChangedHandler OnShuttleChanged;

        public YoyoDataService(IWebHostEnvironment environment, YoyoTimer timer)
        {
            _hostingEnvironment = environment;
            _yoyoTimer = timer;
            LoadBeepTestData();
        }

        private void LoadBeepTestData()
        {
            var dataFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "data", "fitnessrating_beeptest.json");
            var jsonString = File.ReadAllText(dataFilePath);
            this._shuttles = JsonSerializer.Deserialize<Shuttle[]>(jsonString);
        }

        public void Start()
        {
            NotifyShuttleChanged(_shuttles[0]);
            _yoyoTimer.OnElapsed += () =>
            {
                _elapsedTime = _elapsedTime.Add(new TimeSpan(0, 0, 1));

                Console.WriteLine($"Elapsed Time: {_elapsedTime:mm\\:ss}");

                var matchedShuttle = Array.Find(_shuttles, s => s.StartTime == _elapsedTime.ToString(@"mm\:ss"));
                if (matchedShuttle != null)
                {
                    NotifyShuttleChanged(matchedShuttle);
                    Console.WriteLine($"Matched Shuttle: {JsonSerializer.Serialize(matchedShuttle)}");
                }
            };
        }

        private void NotifyShuttleChanged(Shuttle s)
        {
            OnShuttleChanged?.Invoke(s);
        }


    }
}