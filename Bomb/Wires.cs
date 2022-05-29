using System.Collections.Generic;
using System.Linq;

namespace KTANE_Bot
{
    public class Wires : KTANE_Module
    {
        private List<string> _wires;

        public Wires(Bomb bomb) : base(bomb)
        {
            _wires = new List<string>();
        }

        public void AppendWire(string color)
        {
            _wires.Add(color);
        }

        public override string Solve()
        {
            if (_wires.Count > 6)
                return $"Too many wires given. ({_wires.Count})";
            if (_wires.Count < 3)
                return $"You must give at least 3 wires. ({_wires.Count})";
            
            var redWires = (from wire in _wires where wire == "red" select wire).Count();
            var whiteWires = (from wire in _wires where wire == "white" select wire).Count();
            var blackWires = (from wire in _wires where wire == "black" select wire).Count();
            var yellowWires = (from wire in _wires where wire == "yellow" select wire).Count();
            var blueWires = (from wire in _wires where wire == "blue" select wire).Count();
            
            int index = 0;
            
            switch (_wires.Count)
            {
                case 3:
                    if (redWires == 0)
                        index = 2;
                    else if (_wires.Last() == "white")
                        index = 3;
                    else if (blueWires > 1)
                        index = _wires[0] == "blue" && _wires[1] == "blue" && _wires[2] != "blue" ? 2 : 3;
                    else
                        index = 3;
                    
                    break;
                case 4:
                    if (redWires > 1 && !_bomb.EvenDigit)
                    {
                        for (int i = 1; i < _wires.Count; i++)
                            if (_wires[i] == "red")
                                index = i + 1;
                    }
                    else if (_wires.Last() == "yellow" && redWires == 0) 
                        index = 1;
                    else if (blueWires == 1)
                        index = 1;
                    else if (yellowWires > 1)
                        index = 4;
                    else
                        index = 2;
                    
                    break;
                case 5:
                    if (_wires.Last() == "black" && !_bomb.EvenDigit)
                        index = 4;
                    else if (redWires == 1 && yellowWires > 1)
                        index = 1;
                    else if (blackWires == 0)
                        index = 2;
                    else
                        index = 1;
                    
                    break;
                case 6:
                    if (yellowWires == 0 && !_bomb.EvenDigit)
                        index = 3;
                    else if (yellowWires == 1 && whiteWires > 1)
                        index = 4;
                    else if (redWires == 0)
                        index = 6;
                    else
                        index = 4;
                    
                    break;
            }

            return $"Cut the {IndexToWords(index)} wire.";
        }

        private string IndexToWords(int index)
        {
            if (index == _wires.Count)
                return "last";

            var indexingDict = new Dictionary<int, string>
            {
                { 1, "first" },
                { 2, "second" },
                { 3, "third" },
                { 4, "fourth" },
                { 5, "fifth" },
                { 6, "sixth"}
            };

            return indexingDict[index];
        }
    }
}