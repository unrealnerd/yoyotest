using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Pages
{
    using Microsoft.AspNetCore.Components;
    using web.Models;
    using web.Data;
    using web.Services;

    public partial class Index
    {
        [Inject] YoyoTimer YoyoTimer { get; set; }
        [Inject] YoyoDataService YoyoDataService { get; set; }
        [Inject] Repository<Athlete> AthleteRepository { get; set; }

        public bool Started { get; set; } = false;
        private TimeSpan totalTime { get; set; }
        private TimeSpan currentShuttleTimeLeft { get; set; }
        private Shuttle currentShuttle { get; set; }

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
                Console.WriteLine("incoming shuttle");
            };
            YoyoDataService.Start();
            Started = true;
        }

        private async Task UpdateShuttle(Shuttle newShuttle)
        {
            currentShuttle = newShuttle;
            currentShuttleTimeLeft = TimeSpan.ParseExact(currentShuttle.CommulativeTime, @"mm\:ss", System.Globalization.CultureInfo.InvariantCulture,
                                                  System.Globalization.TimeSpanStyles.None);

            await InvokeAsync(StateHasChanged);
        }

        private async Task Tick()
        {
            totalTime = totalTime.Add(new TimeSpan(0, 0, 1));
            currentShuttleTimeLeft = currentShuttleTimeLeft.Add(new TimeSpan(0, 0, -1));

            await InvokeAsync(StateHasChanged);
        }
    }
}
