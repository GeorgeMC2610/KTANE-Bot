using System.Collections.Generic;
using System.Text;

namespace KTANE_Bot
{
    public class Simon : KTANE_Module
    {
        private int _strikes;
        private List<string> _sequence;

        //VOWEL
        private static readonly Dictionary<string, string> VowelNoStrikesDict = new Dictionary<string, string>
        {
            { "red", "blue" },
            { "blue", "red" },
            { "green", "yellow" },
            { "yellow", "green" }
        };
        
        private static readonly Dictionary<string, string> VowelOneStrikeDict = new Dictionary<string, string>
        {
            { "red", "yellow" },
            { "blue", "green" },
            { "green", "blue" },
            { "yellow", "red" }
        };
        
        private static readonly Dictionary<string, string> VowelTwoStrikesDict = new Dictionary<string, string>
        {
            { "red", "green" },
            { "blue", "red" },
            { "green", "yellow" },
            { "yellow", "blue" }
        };
        
        //NO VOWEL
        private static readonly Dictionary<string, string> NoVowelNoStrikesDict = new Dictionary<string, string>
        {
            { "red", "blue" },
            { "blue", "yellow" },
            { "green", "green" },
            { "yellow", "red" }
        };
        
        private static readonly Dictionary<string, string> NoVowelOneStrikeDict = new Dictionary<string, string>
        {
            { "red", "red" },
            { "blue", "blue" },
            { "green", "yellow" },
            { "yellow", "green" }
        };
        
        private static readonly Dictionary<string, string> NoVowelTwoStrikesDict = new Dictionary<string, string>
        {
            { "red", "yellow" },
            { "blue", "green" },
            { "green", "blue" },
            { "yellow", "red" }
        };
        
        

        public Simon(Bomb bomb) : base(bomb)
        {
            _strikes = 0;
            _sequence = new List<string>();
        }

        public override string Solve()
        {
            var buttonsToPress = new StringBuilder();
            Dictionary<string, string> targetDict;

            switch (_strikes)
            {
                case 0 when _bomb.Vowel:
                    targetDict = VowelNoStrikesDict;
                    break;
                case 0 when !_bomb.Vowel:
                    targetDict = NoVowelNoStrikesDict;
                    break;
                case 1 when _bomb.Vowel:
                    targetDict = VowelOneStrikeDict;
                    break;
                case 1 when !_bomb.Vowel:
                    targetDict = NoVowelOneStrikeDict;
                    break;
                case 2 when _bomb.Vowel:
                    targetDict = VowelTwoStrikesDict;
                    break;
                case 2 when !_bomb.Vowel:
                    targetDict = NoVowelTwoStrikesDict;
                    break;
                default:
                    return @"Something is wrong.";
            }

            try
            {
                foreach (var s in _sequence)
                    buttonsToPress.Append($"{targetDict[s]} ");

                return $"Press {buttonsToPress.ToString()}.";
            }
            catch (KeyNotFoundException)
            {
                return "Something is really wrong.";
            }
            
        }

        public void SetStrikes(int strikes)
        {
            _strikes = strikes;
        }

        public void AppendColor(string color)
        {
            _sequence.Add(color);
        }
    }
}