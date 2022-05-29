namespace KTANE_Bot
{
    public class Memory : KTANE_Module
    {
        private static int stage = 1;
        private static int[,] values = new int[5,5];

        public static int Solve(int[] sequence)
        {
            //the values have to be in a specific length
            if (sequence.Length != 5)
                return -1;

            //copy the values to the two-dimensional array
            for (var i = 0; i < sequence.Length; i++)
                values[stage - 1, i] = sequence[i];
            
            //return the number to be pressed.
            switch (stage)
            {
                case 1:
                    if (values[0, 0] == 1 || values[0, 0] == 2) return values[0, 2];
                    if (values[0, 0] == 3) return values[0, 3];
                    return values[0, 4];
                case 2:
                    if (values[1, 0] == 1) return 4;
                    if (values[1, 0] == 2) return values[0, 0];
                    return 6;
            }

            return 2;
        }

        internal Memory(Bomb bomb) : base(bomb)
        {
        }

        public override string Solve()
        {
            throw new System.NotImplementedException();
        }
    }
}