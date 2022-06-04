using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            public bool Marked { get; set; }

            public Block(bool up, bool down, bool right, bool left)
            {
                Up = up;
                Down = down;
                Right = right;
                Left = left;
                Marked = false;
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
        private int[] _circles;

        public Maze(Bomb bomb) : base(bomb)
        {
            _visitedPoints = new Queue<Point>();
            _circles = new[] {-1, -1, -1, -1};
        }

        public override string Solve()
        {
            if (_squareLocation.IsEmpty || _triangleLocation.IsEmpty)
                return @"Something is wrong.";

            _visitedPoints.Enqueue(_triangleLocation);
            Maze1[_triangleLocation.X, _triangleLocation.Y].Marked = true;
            var moves = new List<Point>();

            while (_visitedPoints.Count != 0)
            {
                var cell = _visitedPoints.Peek();
                moves.Add(cell);
                
                _visitedPoints.Dequeue();
                
                //move right (this is y+1)
                if (cell.Y + 1 < 6 && !Maze1[cell.X, cell.Y + 1].Marked && Maze1[cell.X, cell.Y + 1].Right)
                {
                    _visitedPoints.Enqueue(new Point(cell.X, cell.Y + 1));
                    Maze1[cell.X, cell.Y + 1].Marked = true;
                }
                
                //move down (this is (x+1)
                if (cell.X + 1 < 6 && !Maze1[cell.X + 1, cell.Y].Marked && Maze1[cell.X + 1, cell.Y].Down)
                {
                    _visitedPoints.Enqueue(new Point(cell.X + 1, cell.Y));
                    Maze1[cell.X + 1, cell.Y].Marked = true;
                }
                
                //move left (this is y-1)
                if (cell.Y - 1 >= 0 && !Maze1[cell.X, cell.Y - 1].Marked && Maze1[cell.X, cell.Y - 1].Left)
                {
                    _visitedPoints.Enqueue(new Point(cell.X, cell.Y - 1));
                    Maze1[cell.X, cell.Y - 1].Marked = true;
                }
                
                //move up (this is x-1)
                if (cell.X - 1 >= 0 && !Maze1[cell.X - 1, cell.Y].Marked && Maze1[cell.X, cell.Y].Up)
                {
                    _visitedPoints.Enqueue(new Point(cell.X - 1, cell.Y));
                    Maze1[cell.X - 1, cell.Y].Marked = true;
                }
            }
            
            
        }

        public void AssignFirstCircle(int x, int y)
        {
            _circles[0] = --x;
            _circles[1] = --y;
        }

        public void AssignSecondCircle(int x, int y)
        {
            _circles[2] = --x;
            _circles[3] = --y;
        }

        public void SetTriangle(int x, int y)
        {
            _triangleLocation = new Point(--x, --y);
        }

        public void SetSquare(int x, int y)
        {
            _squareLocation = new Point(--x, --y);
        }
    }
}