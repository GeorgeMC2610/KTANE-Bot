namespace KTANE_Bot
{
    public abstract class KTANE_Module
    {
        private protected Bomb _bomb;

        private protected KTANE_Module(Bomb bomb)
        {
            _bomb = bomb;
        }

        public abstract string Solve();
    }
}