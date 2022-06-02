using System;
using System.Collections.Generic;

namespace KTANE_Bot
{
    public class Knob : KTANE_Module
    {
        public string Lights { get; set; }
        
        public Knob(Bomb bomb) : base(bomb)
        {
            Lights = string.Empty;
        }

        private static readonly Dictionary<string, string> PositionsDict = new Dictionary<string, string>
        {
            { "0 0 1 space 1 1 1", "up" },
            { "1 0 1 space 0 1 1", "up" },
            { "0 1 1 space 1 1 1", "down" },
            { "1 0 1 space 0 1 0", "down" },
            { "0 0 0 space 1 0 0", "left" },
            { "0 0 0 space 0 0 0", "left" },
            { "1 0 1 space 1 1 1", "right" }
        };

        public override string Solve()
        {
            return $"{PositionsDict[Lights].ToUpper()} position.";
        }
    }
}