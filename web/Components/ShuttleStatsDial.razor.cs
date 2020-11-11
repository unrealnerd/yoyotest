using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Components
{
    public partial class ShuttleStatsDial
    {
        [Parameter]
        public bool Started { get; set; }
        
        [Parameter]
        public bool Ended { get; set; }

        [Parameter]
        public int? SpeedLevel { get; set; }

        [Parameter]
        public int? ShuttleNumber { get; set; }

        [Parameter]
        public string Speed { get; set; }

        [Parameter] public EventCallback StartTimer { get; set; }

    }
}
