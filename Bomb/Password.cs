using System.Collections.Generic;
using System.Linq;

namespace KTANE_Bot
{
    public class Password : KTANE_Module
    {
        private static readonly string[] Words = ("about,after,again,below,could,every,first,found,great,house,large,learn,never,other,place,plant,point,right,small,sound," +
                                                 "spell,still,study,their,there,these,thing,think,three,water,where,which,world,would,write").Split(',');

        public int Column { get; private set; }

        private List<string> _column1;
        private List<string> _column2;
        private List<string> _column3;
        private List<string> _column4;
        private List<string> _column5;

        public Password(Bomb bomb) : base(bomb)
        {
            Column = 0;
            _column1 = new List<string>();
            _column2 = new List<string>();
            _column3 = new List<string>();
            _column4 = new List<string>();
            _column5 = new List<string>();
        }

        public override string Solve()
        {
            

            return "";
        }

        public int AssignLetters(string words)
        {
            var targetListDict = new Dictionary<int, List<string>>
            {
                { 0, _column1 },
                { 1, _column2 },
                { 2, _column3 },
                { 3, _column4 },
                { 4, _column5 },
            };
            
            var split = words.Split(' ');
            if (split.Length != 6)
                return 1;

            //this translates to "if there are any duplicates".
            if (split.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).Count() != 0)
                return -1;

            //assign the first letter of every word to the letters list.
            foreach (var s in split)
                targetListDict[Column].Add(s);

            //only keep letters that exist in the Words list.
            var existingLettersList = targetListDict[Column].Where(s => (from word in Words where word[Column] == char.Parse(s) select word).Any()).ToList();
            targetListDict[Column] = existingLettersList;

            Column++;
            return 0;
        }
    }
}