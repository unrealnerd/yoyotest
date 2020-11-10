using Microsoft.AspNetCore.Components;

namespace web.Components
{
    public partial class ProgressBar
    {
        private double _percent;

        [Parameter]
        public double Percent
        {
            get
            {
                return ((100 - _percent) / 100) * (2 * 3.14 * Radius);
            }
            set
            {
                _percent = value;
            }
        }

        private int Radius = 70;

    }
}
