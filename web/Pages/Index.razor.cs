using System;
using System.Threading.Tasks;

namespace web.Pages
{
    using Microsoft.AspNetCore.Components;
    using web.Models;
    using web.Data;
    using web.Services;
    using Microsoft.Extensions.Logging;
    using System.Linq;

    public partial class Index
    {
        [Inject] YoyoTimer YoyoTimer { get; set; }
        [Inject] YoyoDataService YoyoDataService { get; set; }
        [Inject] Repository<Athlete> AthleteRepository { get; set; }
        [Inject] ILogger<Index> _logger { get; set; }

        public bool Started { get; set; } = false;
        private TimeSpan TotalTime { get; set; }
        private TimeSpan CurrentShuttleTimeLeft { get; set; }
        private Shuttle CurrentShuttle { get; set; }
        private Shuttle PreviousShuttle { get; set; }
        private double PercentageRemaingInCurrentLevel { get; set; }

        private void Start()
        {
            YoyoTimer.SetTimer(1000, true);
            YoyoTimer.OnElapsed += async delegate
            {
                await Tick();
            };
            YoyoDataService.OnShuttleChanged += async (newShuttle) =>
            {

                await UpdateShuttle(newShuttle);
                _logger.LogDebug("shuttle changed");
            };
            YoyoDataService.Start();
            Started = true;
        }

        private async Task UpdateShuttle(Shuttle newShuttle)
        {
            PreviousShuttle = CurrentShuttle;
            CurrentShuttle = newShuttle;
            CurrentShuttleTimeLeft =
                TimeSpan.ParseExact(
                CurrentShuttle.CommulativeTime, @"mm\:ss",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.TimeSpanStyles.None) -
                TimeSpan.ParseExact(
                CurrentShuttle.StartTime, @"mm\:ss",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.TimeSpanStyles.None).Add(new TimeSpan(0, 0, -1));
            //TimeSpan.FromSeconds(double.Parse(CurrentShuttle.LevelTime));

            await InvokeAsync(StateHasChanged);
        }

        private async Task Tick()
        {
            TotalTime = TotalTime.Add(new TimeSpan(0, 0, 1));
            CurrentShuttleTimeLeft = CurrentShuttleTimeLeft.Add(new TimeSpan(0, 0, -1));
            _logger.LogDebug($"Time Left: {CurrentShuttleTimeLeft}");

            PercentageRemaingInCurrentLevel = (
                CurrentShuttleTimeLeft.TotalSeconds /
                (TimeSpan.ParseExact(
                CurrentShuttle.CommulativeTime, @"mm\:ss",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.TimeSpanStyles.None) -
                TimeSpan.ParseExact(
                CurrentShuttle.StartTime, @"mm\:ss",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.TimeSpanStyles.None).Add(new TimeSpan(0, 0, -1))).TotalSeconds) *
                100;

            await InvokeAsync(StateHasChanged);
        }

        private void Warn(int id)
        {
            var athlete = AthleteRepository.Data.FirstOrDefault(i => i.Id == id);

            athlete.Warned = true;
            _logger.LogInformation($"Warned {athlete.Name}!");
        }

        private void Stop(int id)
        {
            var athlete = AthleteRepository.Data.FirstOrDefault(i => i.Id == id);

            athlete.Result = $"{PreviousShuttle?.SpeedLevel}-{PreviousShuttle?.ShuttleNo}";

            _logger.LogInformation($"Stopped! {athlete.Name}, Result: {athlete.Result}");
        }
    }
}
