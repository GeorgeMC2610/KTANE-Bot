using System.Collections.Generic;

namespace KTANE_Bot
{
    public class ComplexWires : KTANE_Module
    {

        private bool _red;
        private bool _blue;
        private bool _star;
        private bool _light;

        public ComplexWires(Bomb bomb) : base(bomb)
        {
            
        }

        public void InterpretInput(string wire)
        {
            _red = wire.Contains("red");
            _blue = wire.Contains("blue");
            _star = wire.Contains("star");
            _light = wire.Contains("light");
        }

        public override string Solve()
        {
            if (!_red && !_blue && !_star && !_light)
                return "Yes"; //C
            if (!_red && !_blue && _star && !_light)
                return "Yes"; //C
            if (_red && !_blue && _star && !_light)
                return "Yes"; //C
            if (!_red && !_blue && !_star && _light)
                return "No";  //D
            if (_red && _blue && _star && _light)
                return "No";  //D
            if (!_red && _blue && _star && !_light)
                return "No";  //D
            if (_red && _blue && !_star && !_light)
                return _bomb.EvenDigit ? "Yes" : "No"; //S
            if (_red && _blue && !_star && _light)
                return _bomb.EvenDigit ? "Yes" : "No"; //S
            if (_red && !_blue && !_star && !_light)
                return _bomb.EvenDigit ? "Yes" : "No"; //S
            if (!_red && _blue && !_star && !_light)
                return _bomb.EvenDigit ? "Yes" : "No"; //S
            if (_red && _blue && _star && !_light)
                return _bomb.Parallel ? "Yes" : "No"; //P
            if (!_red && _blue && _star && _light)
                return _bomb.Parallel ? "Yes" : "No"; //P
            if (!_red && _blue && !_star && _light)
                return _bomb.Parallel ? "Yes" : "No"; //P
            if (_red && !_blue && _star && _light)
                return _bomb.Batteries > 1 ? "Yes" : "No"; //B
            if (_red && !_blue && !_star && _light)
                return _bomb.Batteries > 1 ? "Yes" : "No"; //B
            if (!_red && !_blue && _star && _light)
                return _bomb.Batteries > 1 ? "Yes" : "No"; //B

            return "Something is wrong.";
        }
    }
}