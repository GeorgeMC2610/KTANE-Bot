using System;
using System.Collections.Generic;

namespace KTANE_Bot
{
    public class WhoIsOnFirst : KTANE_Module
    {
        public string Display { get; set; }
        public string Button { get; set; }

        private static readonly Dictionary<string, string> PositionsDict = new Dictionary<string, string>
        {
            { "yes", "middle left" },
            { "first", "upper right" },
            { "display", "lower right" },
            { "okay", "upper right" },
            { "says", "lower right" },
            { "nothing", "middle left" },
            { "it is blank", "lower left" },
            { "blank", "middle right" },
            { "no", "lower right" },
            { "led", "middle left" },
            { "lead", "lower right" },
            { "read noun", "middle right" },
            { "red colour", "middle right" },
            { "ar ee ee dee", "lower left" },
            { "el ee ee dee", "lower left" },
            { "hold on", "lower right" },
            { "you", "middle right" },
            { "you are", "lower right" },
            { "your pronoun", "middle right" },
            { "you're apostrophe", "middle right" },
            { "u r letters", "upper left" },
            { "there", "lower right" },
            { "they're apostrophe", "lower left" },
            { "their pronoun", "middle right" }
        };

        private static readonly Dictionary<string, string> WordsDict = new Dictionary<string, string>
        {
            { "ready", "YES, OKAY, WHAT, MIDDLE, LEFT, PRESS, RIGHT, BLANK, READY, NO, FIRST, U H H H, NOTHING, WAIT" },
            { "first", "LEFT, OKAY, YES, MIDDLE, NO, RIGHT, NOTHING, U H H H, WAIT, READY, BLANK, WHAT, PRESS, FIRST" },
            { "no", "BLANK, U H H H, WAIT, FIRST, WHAT, READY, RIGHT, YES, NOTHING, LEFT, PRESS, OKAY, NO" },
            { "blank", "WAIT, RIGHT, OKAY, MIDDLE, BLANK" },
            { "nothing", "U H H H, RIGHT, OKAY, MIDDLE, YES, BLANK, NO, PRESS, LEFT, WHAT, WAIT, FIRST, NOTHING" },
            { "yes", "OKAY, RIGHT, U H H H, MIDDLE, FIRST, WHAT, PRESS, READY, NOTHING, YES" },
            { "what", "U H H H, WHAT" },
            { "u h h h", "READY, NOTHING, LEFT, WHAT, OKAY, YES, RIGHT, NO, PRESS, BLANK, U H H H" },
            { "left", "RIGHT, LEFT" },
            { "right", "YES, NOTHING, READY, PRESS, NO, WAIT, WHAT, RIGHT" },
            { "middle", "BLANK, READY, OKAY, WHAT, NOTHING, PRESS, NO, WAIT, LEFT, MIDDLE" },
            { "okay", "MIDDLE, NO, FIRST, YES, U H H H, NOTHING, WAIT, OKAY" },
            { "wait", "U H H H, NO, BLANK, OKAY, YES, LEFT, FIRST, PRESS, WHAT, WAIT" },
            { "press", "RIGHT, MIDDLE, YES, READY, PRESS" },
            {
                "you",
                "SURE, YOU ARE, YOUR PRONOUN, YOU'RE APOSTROPHE, NEXT, U H SPACE H U H, UR LETTERS, HOLD, WHAT QUESTION MARK, YOU"
            },
            {
                "you are",
                "YOUR PRONOUN, NEXT, LIKE, U H SPACE H U H, WHAT QUESTION MARK, DONE, U H SPACE U H, HOLD, YOU, U LETTER, YOU'RE APOSTROPHE, SURE, UR LETTERS, YOU ARE"
            },
            { "your pronoun", "U H SPACE U H, YOU ARE, U H SPACE H U H, YOUR PRONOUN" },
            { "you're apostrophe", "YOU, YOU'RE APOSTROPHE" },
            { "u r letters", "DONE, U LETTER, UR LETTERS" },
            {
                "u letter",
                "U H SPACE H U H, SURE, NEXT, WHAT QUESTION MARK, YOU'RE APOSTROPHE, UR LETTERS, U H SPACE U H, DONE, U LETTER"
            },
            { "u h space h u h", "U H SPACE H U H" },
            { "u h space u h", "UR LETTERS, U LETTER, YOU ARE, YOU'RE APOSTROPHE, NEXT, U H SPACE U H" },
            {
                "what question mark",
                "YOU, HOLD, YOU'RE APOSTROPHE, YOUR PRONOUN, U LETTER, DONE, U H SPACE U H, LIKE, YOU ARE, U H SPACE H U H, UR LETTERS, NEXT, WHAT QUESTION MARK"
            },
            {
                "done",
                "SURE, U H SPACE H U H, NEXT, WHAT QUESTION MARK, YOUR PRONOUN, UR LETTERS, YOU'RE APOSTROPHE, HOLD, LIKE, YOU, U LETTER, YOU ARE, U H SPACE U H, DONE"
            },
            { "next", "WHAT QUESTION MARK, U H SPACE H U H, U H SPACE U H, YOUR PRONOUN, HOLD, SURE, NEXT" },
            {
                "hold",
                "YOU ARE, U LETTER, DONE, U H SPACE U H, YOU, UR LETTERS, SURE, WHAT QUESTION MARK, YOU'RE APOSTROPHE, NEXT, HOLD"
            },
            { "sure", "YOU ARE, DONE, LIKE, YOU'RE APOSTROPHE, YOU, HOLD, U H SPACE H U H, UR LETTERS, SURE" },
            {
                "like",
                "YOU'RE APOSTROPHE, NEXT, U LETTER, UR LETTERS, HOLD, DONE, U H SPACE U H, WHAT QUESTION MARK, U H SPACE H U H, YOU, LIKE"
            }
        };

        public WhoIsOnFirst(Bomb bomb) : base(bomb)
        {
            Display = string.Empty;
            Button = string.Empty;
        }

        public override string Solve()
        {
            return Button == string.Empty ? PositionsDict[Display] : WordsDict[Button];
        }
    }
}