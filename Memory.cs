namespace KTANE_Bot
{
    public class Memory : Bomb
    {
        private int[] Stage_1;
        private int[] Stage_2;
        private int[] Stage_3;
        private int[] Stage_4;

        public Memory(int batteries, bool vowel, bool parallel, bool evenDigit, bool frk, bool car) : base(batteries, vowel, parallel, evenDigit, frk, car)
        {
            Stage_1 = Stage_2 = Stage_3 = Stage_4 = new int[4];
        }
    }
}