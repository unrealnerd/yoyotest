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
        public string Result { get; set; }
        public bool CanEdit { get; set; }
    }
}
