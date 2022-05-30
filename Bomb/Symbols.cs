using System.Collections.Generic;
using System.Linq;

namespace KTANE_Bot
{
    public class Symbols : KTANE_Module
    {
        private static readonly Dictionary<string, int> FirstColumn = new Dictionary<string, int>
        {
            { "capital queue", 0 },
            { "capital ay", 1 },
            { "lambda", 2 },
            { "lightning", 3 },
            { "kitty", 4 },
            { "kappa", 5 },
            { "reverse dotted c", 6 }
        };

        private static readonly Dictionary<string, int> SecondColumn = new Dictionary<string, int>
        {
            { "e with umlaut", 0 },
            { "capital queue", 1 },
            { "reverse dotted c", 2 },
            { "snake", 3 },
            { "empty star", 4 },
            { "kappa", 5 },
            { "question mark", 6 }
        };

        private static readonly Dictionary<string, int> ThirdColumn = new Dictionary<string, int>
        {
            { "copyright", 0 },
            { "omega", 1 },
            { "snake", 2 },
            { "ex eye", 3 },
            { "three", 4 },
            { "lambda", 5 },
            { "empty star", 6 }
        };

        private static readonly Dictionary<string, int> FourthColumn = new Dictionary<string, int>
        {
            { "six", 0 },
            { "paragraph", 1 },
            { "tampa bay", 2 },
            { "kitty", 3 },
            { "ex eye", 4 },
            { "question mark", 5 },
            { "smiley face", 6 }
        };

        private static readonly Dictionary<string, int> FifthColumn = new Dictionary<string, int>
        {
            { "psi", 0 },
            { "smiley face", 1 },
            { "tampa bay", 2 },
            { "dotted c", 3 },
            { "paragraph", 4 },
            { "evil three", 5 },
            { "full star", 6 }
        };

        private static readonly Dictionary<string, int> SixthColumn = new Dictionary<string, int>
        {
            { "six", 0 },
            { "e with umlaut", 1 },
            { "dumbbell", 2 },
            { "ay ee", 3 },
            { "psi", 4 },
            { "reverse n", 5 },
            { "capital omega", 6 }
        };

        private List<string> _input;
        public int InputLength => _input.Count;


        public Symbols(Bomb bomb) : base(bomb)
        {
            _input = new List<string>();
        }

        public override string Solve()
        {
            var firstKeys = FirstColumn.Keys.ToList();
            var secondKeys = SecondColumn.Keys.ToList();
            var thirdKeys = ThirdColumn.Keys.ToList();
            var fourthKeys = FourthColumn.Keys.ToList();
            var fifthKeys = FifthColumn.Keys.ToList();
            var sixthKeys = SixthColumn.Keys.ToList();

            Dictionary<string, int> targetDict;

            if (_input.All(x => firstKeys.Contains(x)))
                targetDict = FirstColumn;
            else if (_input.All(x => secondKeys.Contains(x)))
                targetDict = SecondColumn;
            else if (_input.All(x => thirdKeys.Contains(x)))
                targetDict = ThirdColumn;
            else if (_input.All(x => fourthKeys.Contains(x)))
                targetDict = FourthColumn;
            else if (_input.All(x => fifthKeys.Contains(x)))
                targetDict = FifthColumn;
            else if (_input.All(x => sixthKeys.Contains(x)))
                targetDict = SixthColumn;
            else
                return "Wrong sequence.";

            var output = _input.OrderBy(x => targetDict[x]).ToList();

            return $"First is {output[0]}; then {output[1]}; then {output[2]}; then {output[3]}.";
        }

        public void AppendSymbol(string symbol)
        {
            if (_input.Count == 4) return;

            _input.Add(symbol);
        }
    }
}