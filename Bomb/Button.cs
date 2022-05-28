using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace KTANE_Bot
{
    public class Button : KTANE_Module
    {
        private readonly string _color;
        private readonly string _label;

        private const string Hold = "Hold it; tell me the stripe colour.";
        private const string Press = "Press and immediately release.";

        public Button(Bomb bomb, string color, string label) : base(bomb)
        {
            _bomb = bomb;
            _color = color;
            _label = label;
        }

        public override string Solve()
        {
            if (_color == "Blue" && _label == "abort")        return Hold;
            if (_bomb.Batteries > 1 && _label == "detonate") return Press;
            if (_bomb.CAR && _color == "White")              return Hold;
            if (_bomb.FRK && _bomb.Batteries > 2)           return Press;
            if (_color == "Yellow")                          return Hold;
            if (_color == "Red" && _label == "hold")          return Press;
            
            return Hold;
        }

        public static string Solve(string color)
        {
            var solvingDict = new Dictionary<string, string>
            {
                { "Blue", "Release at four." },
                { "Yellow", "Release at five." }
            };

            try
            {
                return solvingDict[color];
            }
            catch (KeyNotFoundException)
            {
                return "Release at one.";
            }
        }
    }
}