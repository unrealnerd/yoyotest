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
        [Inject] ILogger<Index> Logger { get; set; }

        public bool Started { get; set; } = false;
        private TimeSpan TotalTime { get; set; }
        private TimeSpan CurrentShuttleTimeLeft { get; set; }
        private Shuttle CurrentShuttle { get; set; }
        private Shuttle PreviousShuttle { get; set; }

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
            PreviousShuttle = CurrentShuttle;
            CurrentShuttle = newShuttle;
            CurrentShuttleTimeLeft = TimeSpan.ParseExact(CurrentShuttle.CommulativeTime, @"mm\:ss", System.Globalization.CultureInfo.InvariantCulture,
                                                  System.Globalization.TimeSpanStyles.None);

            await InvokeAsync(StateHasChanged);
        }

        private async Task Tick()
        {
            TotalTime = TotalTime.Add(new TimeSpan(0, 0, 1));
            CurrentShuttleTimeLeft = CurrentShuttleTimeLeft.Add(new TimeSpan(0, 0, -1));

            await InvokeAsync(StateHasChanged);
        }

        private void Warn(int id)
        {
            var athlete = AthleteRepository.Data.FirstOrDefault(i => i.Id == id);

            athlete.Warned = true;
            Logger.LogInformation($"Warned {athlete.Name}!");
        }

        private void Stop(int id)
        {
            var athlete = AthleteRepository.Data.FirstOrDefault(i => i.Id == id);

            athlete.Result = $"{PreviousShuttle?.SpeedLevel}-{PreviousShuttle?.ShuttleNo}";

            Logger.LogInformation($"Stopped! {athlete.Name}, Result: {athlete.Result}");
        }
    }
}
