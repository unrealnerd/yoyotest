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
    using System.Timers;

    public partial class Index
    {
        [Inject] YoyoTimer YoyoTimer { get; set; }
        [Inject] YoyoDataService YoyoDataService { get; set; }
        [Inject] ILogger<Index> _logger { get; set; }

        public bool Started { get; set; } = false;
        private TimeSpan TotalTime { get; set; }
        private TimeSpan CurrentShuttleTimeLeft { get; set; }
        private Shuttle CurrentShuttle { get; set; }
        private Shuttle PreviousShuttle { get; set; }
        private double PercentageRemaingInCurrentLevel { get; set; }

        protected override Task OnInitializedAsync()
        {
            YoyoTimer.Stop();
            return base.OnInitializedAsync();
        }

        private void StartTimer()
        {
            YoyoTimer.Start();
            YoyoTimer.OnElapsed += Tick;
            Started = true;
        }

        private async void UpdateShuttle()
        {
            var newShuttle = YoyoDataService.CheckForMatchingShuttle(TotalTime);

            if (newShuttle == null) return;

            _logger.LogDebug("Shuttle Changed!");

            PreviousShuttle = CurrentShuttle;
            CurrentShuttle = newShuttle;
            CurrentShuttleTimeLeft = CurrentShuttle.CommulativeTime - CurrentShuttle.StartTime;

            await InvokeAsync(StateHasChanged);
        }

        private async void Tick()
        {
            UpdateShuttle();

            _logger.LogDebug($"Total Time: {TotalTime}");
            _logger.LogDebug($"Time Left: {CurrentShuttleTimeLeft}");

            PercentageRemaingInCurrentLevel =
                (CurrentShuttleTimeLeft.TotalSeconds /
                    (CurrentShuttle.CommulativeTime - CurrentShuttle.StartTime)
                .TotalSeconds) * 100;

            TotalTime = TotalTime.Add(new TimeSpan(0, 0, 1));
            CurrentShuttleTimeLeft = CurrentShuttleTimeLeft.Add(new TimeSpan(0, 0, -1));

            await InvokeAsync(StateHasChanged);
        }

    }
}
