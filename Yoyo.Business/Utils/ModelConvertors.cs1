using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web.Models;
using web.ViewModels;

namespace web.Utils
{
    public static class ModelConvertors
    {
        public static AthleteViewModel ToAthleteViewModel(this Athlete athlete)
        {
            return new AthleteViewModel
            {
                Id = athlete.Id,
                Name = athlete.Name,
                Warned = false
            };
        }
    }
}
