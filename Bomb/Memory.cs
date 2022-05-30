using System.Collections.Generic;

namespace KTANE_Bot
{
    public class Memory : KTANE_Module
    {
        public int Stage { get; private set; }

        private int _display;
        private int _firstNumber;
        private int _secondNumber;
        private int _thirdNumber;
        private int _fourthNumber;

        private static Dictionary<string, int> _numbersDict = new Dictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 }
        };

        public Memory(Bomb bomb) : base(bomb)
        {
            Stage = 1;
        }

        public override string Solve()
        {
            var numberToPress = 0;
            
            switch (Stage)
            {
                
            }
        }

        public bool SetNumbers(params string[] numbers)
        {
            if (numbers.Length != 6)
                return false;
            
            try
            {
                _display = _numbersDict[numbers[1]];
                _firstNumber = _numbersDict[numbers[2]];
                _secondNumber = _numbersDict[numbers[3]];
                _thirdNumber = _numbersDict[numbers[4]];
                _fourthNumber = _numbersDict[numbers[5]];
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }
    }
}