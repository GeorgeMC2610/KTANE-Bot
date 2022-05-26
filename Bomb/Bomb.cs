using System.Linq;

namespace KTANE_Bot
{
    public class Bomb
    {
        //bomb properties
        public readonly int Batteries;
        public readonly bool Parallel;
        public readonly bool FRK;
        public readonly bool CAR;
        public readonly bool Vowel;
        public readonly bool EvenDigit;

        public Bomb(int batteries, bool parallel, bool frk, bool car, bool vowel, bool evenDigit)
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