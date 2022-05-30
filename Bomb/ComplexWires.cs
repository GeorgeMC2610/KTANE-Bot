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

        public bool InterpretInput(string wire)
        {
            
        }

        public override string Solve()
        {
            //don't look at me, Rider did this.
            switch (_red)
            {
                case false when !_blue && !_star && !_light:
                //C
                case false when !_blue && _star && !_light:
                //C
                case true when !_blue && _star && !_light:
                    return "Yes"; //C
                case false when !_blue && !_star && _light:
                //D
                case true when _blue && _star && _light:
                //D
                case false when _blue && _star && !_light:
                    return "No";  //D
                case true when _blue && !_star && !_light:
                    return _bomb.EvenDigit ? "Yes" : "No"; //S
                case true when _blue && !_star && _light:
                    return _bomb.EvenDigit ? "Yes" : "No"; //S
                case true when !_blue && !_star && !_light:
                    return _bomb.EvenDigit ? "Yes" : "No"; //S
                case false when _blue && !_star && !_light:
                    return _bomb.EvenDigit ? "Yes" : "No"; //S
                case true when _blue && _star && !_light:
                    return _bomb.Parallel ? "Yes" : "No"; //P
                case false when _blue && _star && _light:
                    return _bomb.Parallel ? "Yes" : "No"; //P
                case false when _blue && !_star && !_light:
                    return _bomb.Parallel ? "Yes" : "No"; //P
                case true when !_blue && _star && _light:
                    return _bomb.Batteries > 1 ? "Yes" : "No"; //B
                case true when !_blue && !_star && _light:
                    return _bomb.Batteries > 1 ? "Yes" : "No"; //B
                case false when !_blue && _star && _light:
                    return _bomb.Batteries > 1 ? "Yes" : "No"; //B
                default:
                    return "Something is wrong.";
            }
        }
    }
}