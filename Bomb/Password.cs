using System.Collections.Generic;

namespace KTANE_Bot
{
    public class Password : KTANE_Module
    {
        private static readonly string[] Words = ("about,after,again,below,could,every,first,found,great,house,large,learn,never,other,place,plant,point,right,small,sound," +
                                                 "spell,still,study,their,there,these,thing,think,three,water,where,which,world,would,write").Split(',');

        private List<string> column1;
        private List<string> column2;
        private List<string> column3;

        public Password(Bomb bomb) : base(bomb)
        {
            column1 = new List<string>();
            column2 = new List<string>();

        }

        public override string Solve()
        {
            throw new System.NotImplementedException();
        }
    }
}