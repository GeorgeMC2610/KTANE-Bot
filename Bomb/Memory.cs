using System;
using System.Collections.Generic;

namespace KTANE_Bot
{
    public class Memory : KTANE_Module
    {
        public int Stage { get; private set; }

        private int[] _sequence;
        private int[] _positions;
        private int[] _numbers;

        private static readonly Dictionary<string, int> NumbersDict = new Dictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 }
        };

        public Memory(Bomb bomb) : base(bomb)
        {
            Stage = 1;
            _positions = new int[5];
            _numbers = new int[5];
            _sequence = new int[5];
        }

        public override string Solve()
        {
            var numberToPress = 0;
            
            switch (Stage)
            {
                case 1:
                    
                    switch (_sequence[0])
                    {
                        case 1:
                        case 2:
                            _positions[0] = 2;
                            _numbers[0] = _sequence[2];
                            numberToPress = _sequence[2];
                            break;
                        case 3:
                            _positions[0] = 3;
                            _numbers[0] = _sequence[3];
                            numberToPress = _sequence[3];
                            break;
                        default:
                            _positions[0] = 4;
                            _numbers[0] = _sequence[4];
                            numberToPress = _sequence[4];
                            break;
                    }
                    
                    break;
                
                case 2:
                    
                    switch (_sequence[0])
                    {
                        case 1:
                            _positions[1] = Array.IndexOf(_sequence, 4);
                            _numbers[1] = 4;
                            numberToPress = 4;
                            break;
                        case 2:
                            _positions[1] = _positions[0];
                            _numbers[1] = _sequence[_positions[0]];
                            numberToPress = _sequence[_positions[0]];
                            break;
                        case 3:
                            _positions[1] = 1;
                            _numbers[1] = _sequence[1];
                            numberToPress = _sequence[1];
                            break;
                        default:
                            _positions[1] = _positions[0];
                            _numbers[1] = _sequence[_positions[0]];
                            numberToPress = _sequence[_positions[0]];
                            break;
                    }

                    break;
                
                case 3:

                    switch (_sequence[0])
                    {
                        case 1:
                            _positions[2] = Array.IndexOf(_sequence, _numbers[1]);
                            _numbers[2] = _numbers[1];
                            numberToPress = _numbers[1];
                            break;
                        case 2:
                            _positions[2] = Array.IndexOf(_sequence, _numbers[0]);
                            _numbers[2] = _numbers[0];
                            numberToPress = _numbers[0];
                            break;
                        case 3:
                            _positions[2] = 3;
                            _numbers[2] = _sequence[3];
                            numberToPress = _sequence[3];
                            break;
                        default:
                            _positions[2] = Array.IndexOf(_sequence, 4);
                            _numbers[2] = 4;
                            numberToPress = 4;
                            break;
                    }

                    break;
                
                case 4:

                    switch (_sequence[0])
                    {
                        case 1:
                            _positions[3] = _positions[0];
                            _numbers[3] = _sequence[_positions[0]];
                            numberToPress = _sequence[_positions[0]];
                            break;
                        case 2:
                            _positions[3] = 1;
                            _numbers[3] = _sequence[1];
                            numberToPress = _sequence[1];
                            break;
                        case 3:
                            _positions[3] = _positions[1];
                            _numbers[3] = _sequence[_positions[1]];
                            numberToPress = _sequence[_positions[1]];
                            break;
                        default:
                            _positions[3] = _positions[1];
                            _numbers[3] = _sequence[_positions[1]];
                            numberToPress = _sequence[_positions[1]];
                            break;
                    }

                    break;
                
                case 5:

                    switch (_sequence[0])
                    {
                        case 1:
                            numberToPress = _numbers[0];
                            break;
                        case 2:
                            numberToPress = _numbers[1];
                            break;
                        case 3:
                            numberToPress = _numbers[3];
                            break;
                        default:
                            numberToPress = _numbers[2];
                            break;
                    }
                    
                    break;
                
            }

            Stage++;
            return $"Press {numberToPress}, " + (Stage != 6? $"stage {Stage}" : "done");
        }

        public bool SetNumbers(params string[] numbers)
        {
            if (numbers.Length != 6)
                return false;
            
            try
            {
                _sequence[0] = NumbersDict[numbers[1]];
                _sequence[1] = NumbersDict[numbers[2]];
                _sequence[2] = NumbersDict[numbers[3]];
                _sequence[3] = NumbersDict[numbers[4]];
                _sequence[4] = NumbersDict[numbers[5]];
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }
    }
}