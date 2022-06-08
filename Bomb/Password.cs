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
            var combinationLetters = new List<string>();

            foreach (var t1 in _column1)
            {
                combinationLetters.Add(t1);
                foreach (var t2 in _column2)
                {
                    combinationLetters.Remove(t1);
                    combinationLetters.Add(t1 + t2);
                    foreach (var t3 in _column3)
                    {
                        combinationLetters.Remove(t1 + t2);
                        combinationLetters.Add(t1 + t2 + t3);
                        foreach (var t4 in _column4)
                        {
                            combinationLetters.Remove(t1 + t2 + t3);
                            combinationLetters.Add(t1 + t2 + t3 + t4);
                            foreach (var t5 in _column5)
                            {
                                combinationLetters.Remove(t1 + t2 + t3 + t4);
                                combinationLetters.Add(t1 + t2 + t3 + t4 + t5);
                            }
                        }
                    }
                }
            }

            var possibleWords = (from letter in combinationLetters from word in Words where word.StartsWith(letter) select word).ToList();

            switch (possibleWords.Count)
            {
                case 0:
                    return @"Something is wrong.";
                case 1:
                    return $"The password is \"{possibleWords[0]}.\"";
                default:
                    return possibleWords.Count < 6 ? $"Try words: {string.Join(", ", possibleWords)}" : $"Column {Column + 1}.";
            }
        }

        public int AssignLetters(string letter)
        {
            var targetListDict = new Dictionary<int, List<string>>
            {
                { 0, _column1 },
                { 1, _column2 },
                { 2, _column3 },
                { 3, _column4 },
                { 4, _column5 },
            };

            if (letter.Length > 1)
                letter = letter[0].ToString();

            //if there is any duplicate don't allow it.
            if (!targetListDict[Column].Contains(letter))
                targetListDict[Column].Add(letter);
            else
                return -1;

            if (targetListDict[Column].Count == 6)
            {
                Column++;
                return 0;
            }
            
            return 2;

        }
    }
}