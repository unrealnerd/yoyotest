using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Components
{
    public partial class ShuttleStats
    {
        [Parameter]
        public TimeSpan CurrentShuttleTimeLeft { get; set; }

        [Parameter]
        public TimeSpan TotalTime { get; set; }
        
        [Parameter]
        public int AccumulatedShuttleDistance { get; set; }

    }
}
