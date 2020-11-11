using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.ViewModels
{
    public class AthleteViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Warned { get; set; }

        /// <summary>
        /// If this Porperty is null it means the athlete is asked to stop. 
        /// </summary>
        public string Result { get; set; }
    }
}
