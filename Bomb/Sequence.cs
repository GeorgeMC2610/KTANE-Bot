using System.Collections.Generic;
using System.Linq;

namespace KTANE_Bot
{
    public class Sequence : KTANE_Module
    {
        private int _redWires;
        private int _blueWires;
        private int _blackWires;

        private static Dictionary<int, string[]> redOccurences = new Dictionary<int, string[]>
        {
            { 1, new[] {"charlie"}},
            { 2, new[] {"bravo"}},
            { 3, new[] {"alpha"}},
            { 4, new[] {"alpha", "charlie"}},
            { 5, new[] {"bravo"}},
            { 6, new[] {"alpha", "charlie"}},
            { 7, new[] {"alpha", "bravo", "charlie"}},
            { 8, new[] {"alpha", "bravo"}},
            { 9, new[] {"bravo"}},
        };
        
        private static Dictionary<int, string[]> blueOccurences = new Dictionary<int, string[]>
        {
            { 1, new[] {"bravo"}},
            { 2, new[] {"alpha", "charlie"}},
            { 3, new[] {"bravo"}},
            { 4, new[] {"alpha"}},
            { 5, new[] {"bravo"}},
            { 6, new[] {"bravo", "charlie"}},
            { 7, new[] {"charlie"}},
            { 8, new[] {"alpha", "charlie"}},
            { 9, new[] {"alpha"}},
        };
        
        private static Dictionary<int, string[]> blackOccurences = new Dictionary<int, string[]>
        {
            { 1, new[] {"alpha", "bravo", "charlie"}},
            { 2, new[] {"alpha", "charlie"}},
            { 3, new[] {"bravo"}},
            { 4, new[] {"alpha", "charlie"}},
            { 5, new[] {"bravo"}},
            { 6, new[] {"bravo", "charlie"}},
            { 7, new[] {"alpha", "bravo"}},
            { 8, new[] {"charlie"}},
            { 9, new[] {"charlie"}},
        };

        internal Sequence(Bomb bomb) : base(bomb)
        {
            _redWires = 0;
            _blueWires = 0;
            _blackWires = 0;
        }

        private string _color;
        private string _letter;

        public override string Solve()
        {
            Dictionary<int, string[]> targetDictionary = new Dictionary<int, string[]>();
            int targetWireCount = 0;

            switch (_color)
            {
                case "red":
                    _redWires++;
                    targetWireCount = _redWires;
                    targetDictionary = redOccurences;
                    break;
                case "blue":
                    _blueWires++;
                    targetWireCount = _blueWires;
                    targetDictionary = blueOccurences;
                    break;
                case "black":
                    _blackWires++;
                    targetWireCount = _blackWires;
                    targetDictionary = blackOccurences;
                    break;
            }

            return $"{(targetDictionary[targetWireCount].Contains(_letter)? "Yes" : "No")}";
        }

        public void InitializeValues(string color, string letter)
        {
            _color = color;
            _letter = letter;
        }
    }
}