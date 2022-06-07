using System.Collections.Generic;
using System.Linq;

namespace KTANE_Bot
{
    public class Password : KTANE_Module
    {
        private static readonly string[] Words = ("about,after,again,below,could,every,first,found,great,house,large,learn,never,other,place,plant,point,right,small,sound," +
                                                 "spell,still,study,their,there,these,thing,think,three,water,where,which,world,would,write").Split(',');

        public int Column { get; private set; }
        private string[,] _letters;

        public Password(Bomb bomb) : base(bomb)
        {
            Column = 0;
            _letters = new string[6, 5];
        }

        public override string Solve()
        {
            
        }

        public int AssignLetters(string words)
        {
            var split = words.Split(' ');
            if (split.Length != 6)
                return 1;

            //this translates to "if there are any duplicates".
            if (split.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).Count() != 0)
                return -1;

            //assign the first letter of every word to the letters list.
            for (var i = 0; i < 6; i++)
                _letters[i, Column] = split[i][0].ToString();

            Column++;
            return 0;
        }
    }
}