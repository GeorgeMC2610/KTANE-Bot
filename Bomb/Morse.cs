using System.Collections.Generic;

namespace KTANE_Bot
{
    public class Morse : KTANE_Module
    {
        private static readonly Dictionary<string, char> MorseCodeDict = new Dictionary<string, char>
        {
            { "01", 'a' },
            { "1000", 'b' },
            { "1010", 'c' },
            { "100", 'd' },
            { "0", 'e' },
            { "0010", 'f' },
            { "110", 'g' },
            { "0000", 'h' },
            { "00", 'i' },
            { "0111", 'j' },
            { "101", 'k' },
            { "0100", 'l' },
            { "11", 'm' },
            { "10", 'n' },
            { "111", 'o' },
            { "0110", 'p' },
            { "1101", 'q' },
            { "010", 'r' },
            { "000", 's' },
            { "1", 't' },
            { "001", 'u' },
            { "0001", 'v' },
            { "011", 'w' },
            { "1001", 'x' },
            { "1011", 'y' },
            { "1100", 'z' }
        };
        
        private static readonly Dictionary<string, float> WordsDict = new Dictionary<string, float>
        {
            
        }

        public Morse(Bomb bomb) : base(bomb)
        {
            
        }

        public override string Solve()
        {
            
        }
    }
}