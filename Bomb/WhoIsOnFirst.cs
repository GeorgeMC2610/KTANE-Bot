using System.Collections.Generic;

namespace KTANE_Bot
{
    public class WhoIsOnFirst : KTANE_Module
    {
        public string Display { get; set; }
        public string Button { get; set; }

        private static readonly Dictionary<string, string> PositionsDict = new Dictionary<string, string>
        {
            {"yes", "middle left"},
            {"first", "upper right"},
            {"display", "lower right"},
            {"okay", "upper right"},
            {"says", "lower right"},
            {"nothing", "middle left"},
            {"it is blank", "lower left"},
            {"blank", "middle right"},
            {"no", "lower right"}
        }

        public WhoIsOnFirst(Bomb bomb) : base(bomb)
        {
            Display = string.Empty;
            Button = string.Empty;
        }

        public override string Solve()
        {
            return "";
        }
    }
}