using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace KTANE_Bot
{
    public class Button
    {
        private Bomb _bomb;
        private string _color;
        private string _label;

        public Button(Bomb bomb, string color, string label)
        {
            _bomb = bomb;
            _color = color;
            _label = label;
        }
        
        public string Solve()
        {
            if (_color == "Blue" && _label == "abort")       return "Hold, what is the stripe colour?";
            if (_bomb.Batteries > 1 && _label == "detonate") return "Press and immediately release.";
            if (_bomb.CAR && _color == "White")              return "Hold the button, what is the stripe colour?";
            if (_bomb.FRK && _bomb.Batteries > 2)            return "Press and immediately release.";
            if (_color == "Yellow")                          return "Hold the button, what is the stripe colour?";
            if (_color == "Red" && _label == "hold")         return "Press and immediately release.";
            
            return "Hold the button, what is the stripe colour?";
        }

        public string Solve(string color)
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