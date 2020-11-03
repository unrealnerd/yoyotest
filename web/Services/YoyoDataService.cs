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
        private Shuttle[] _shuttles;

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
            _yoyoTimer.OnElapsed += () =>
            {
                Console.WriteLine("Elapsed from YoyoDataService");
            };
        }


    }
}