using System.Linq;

namespace KTANE_Bot
{
    public class Bomb
    {
        //bomb properties
        private readonly int Batteries;
        private readonly bool Vowel;
        private readonly bool Parallel;
        private readonly bool EvenDigit;
        private readonly bool FRK;
        private readonly bool CAR;

        public Bomb(int batteries, bool vowel, bool parallel, bool evenDigit, bool frk, bool car)
        {
            Batteries = batteries;
            Vowel = vowel;
            Parallel = parallel;
            EvenDigit = evenDigit;
            FRK = frk;
            CAR = car;
        }

        public int SimpleWires(params string[] wires)
        {
            return -1;
        }

        public string Button(string color, string label)
        {
            return null;
        }

        public string Symbols()
        {
            return null;
        }
        
        
    }
}