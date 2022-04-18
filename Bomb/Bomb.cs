using System.Linq;

namespace KTANE_Bot
{
    public class Bomb
    {
        //bomb properties
        public readonly int Batteries;
        public readonly bool Vowel;
        public readonly bool Parallel;
        public readonly bool EvenDigit;
        public readonly bool FRK;
        public readonly bool CAR;

        public Bomb(int batteries, bool vowel, bool parallel, bool evenDigit, bool frk, bool car)
        {
            Batteries = batteries;
            Vowel = vowel;
            Parallel = parallel;
            EvenDigit = evenDigit;
            FRK = frk;
            CAR = car;
        }
    }
}