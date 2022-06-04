using System;
using System.Collections.Generic;
using System.Drawing;
using static KTANE_Bot.Maze.Block;

namespace KTANE_Bot
{
    public class Maze : KTANE_Module
    {
        internal class Block
        {
            public bool Down { get; }
            public bool Up { get; }
            public bool Right { get; }
            public bool Left { get; }

            public Block(bool up, bool down, bool right, bool left)
            {
                Up = up;
                Down = down;
                Right = right;
                Left = left;
            }

            public static readonly Block Nothing = new Block(false, false, false, false);
            public static readonly Block OnlyLeft = new Block(false, false, false, true);
            
            public static readonly Block OnlyRight = new Block(false, false, true, false);
            public static readonly Block RightLeft = new Block(false, false, true, true);

            public static readonly Block OnlyDown = new Block(false, true, false, false);
            public static readonly Block DownLeft = new Block(false, true, false, true);
            public static readonly Block DownRight = new Block(false, true, true, false);
            public static readonly Block DownRightLeft = new Block(false, true, true, true);

            public static readonly Block OnlyUp = new Block(true, false, false, false);
            public static readonly Block UpLeft = new Block(true, false, false, true);
            public static readonly Block UpRight = new Block(true, false, true, false);
            public static readonly Block UpRightLeft = new Block(true, false, true, true);
            public static readonly Block UpDown = new Block(true, true, false, false);
            public static readonly Block UpDownLeft = new Block(true, true, false, true);
            public static readonly Block UpDownRight = new Block(true, true, true, false);
            public static readonly Block All = new Block(true, true, true, true);
        }

        private static readonly Block[,] Maze1 =
        {
            { DownRight, RightLeft, DownLeft, DownRight, RightLeft, OnlyLeft },
            { UpDown, DownRight, UpLeft, UpRight, RightLeft, DownLeft },
            { UpDown, UpRight, DownLeft, DownRight, RightLeft, UpDownLeft },
            { UpDown, OnlyRight, UpRightLeft, UpLeft, OnlyRight, UpDownLeft },
            { UpDownRight, RightLeft, DownLeft, DownRight, OnlyLeft, UpDown },
            { UpRight, OnlyLeft, UpRight, UpLeft, OnlyRight, UpLeft }
        };

        private Point _squareLocation;
        private Point _triangleLocation;
        private Queue<Point> _visitedPoints;

        public Maze(Bomb bomb) : base(bomb)
        {
            _visitedPoints = new Queue<Point>();
        }

        public override string Solve()
        {
            return "";
        }

        public void AssignFirstCircle(int x, int y)
        {
            
        }

        public void AssignSecondCircle(int x, int y)
        {
            
        }
    }
}