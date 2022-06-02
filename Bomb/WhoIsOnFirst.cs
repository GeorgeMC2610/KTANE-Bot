namespace KTANE_Bot
{
    public class WhoIsOnFirst : KTANE_Module
    {
        public string Display { get; set; }
        public string Button { get; set; }
        
        public WhoIsOnFirst(Bomb bomb) : base(bomb)
        {
            Display = string.Empty;
            Button = string.Empty;
        }

        public override string Solve()
        {
            return "";
        }
    }
}