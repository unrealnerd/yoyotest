using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web.Data;
using web.Models;
using web.Services;
using web.Utils;
using web.ViewModels;

namespace web.Components
{
    public partial class AthletesList
    {
        [Inject]
        private YoyoDataService YoyoDataService { get; set; }

        [Inject]
        private ILogger<AthletesList> Logger { get; set; }

        [Parameter]
        public Shuttle PreviousShuttle { get; set; }

        [Parameter]
        public bool Started { get; set; }

        [Parameter]
        public bool Ended { get; set; }

        public List<AthleteViewModel> Athletes { get; set; }
        public List<(int, int)> ShuttleResults { get; set; }

        [Parameter]
        public Action OnLastAthleteStopped { get; set; }

        protected override Task OnInitializedAsync()
        {
            Athletes = YoyoDataService.Athletes.Select(x => x.ToAthleteViewModel()).ToList();

            ShuttleResults = YoyoDataService.GetShuttleResults();

            return base.OnInitializedAsync();
        }

        private void Warn(int id)
        {
            var athlete = Athletes.FirstOrDefault(i => i.Id == id);

            athlete.Warned = true;
            Logger.LogInformation($"Warned {athlete.Name}!");
        }

        private void StopAthlete(int id)
        {
            var athlete = Athletes.FirstOrDefault(i => i.Id == id);

            athlete.Result = $"{PreviousShuttle?.SpeedLevel}-{PreviousShuttle?.ShuttleNo}";

            Logger.LogInformation($"Stopped! {athlete.Name}, Result: {athlete.Result}");

            if (!Athletes.Any(x => x.Result == null))
            {
                OnLastAthleteStopped?.Invoke();
            }
        }

    }
}
