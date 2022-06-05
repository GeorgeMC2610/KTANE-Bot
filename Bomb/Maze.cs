using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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
        
        public Point SquareLocation { get; private set; }
        public Point TriangleLocation { get; private set; }
        private int[] _circles;
        private Block[,] _targetMaze;

        public Maze(Bomb bomb) : base(bomb)
        {
            _circles = new[] {-1, -1, -1, -1};
            TriangleLocation = new Point(-1, -1);
            SquareLocation = new Point(-1, -1);
        }

        public override string Solve()
        {
            Block[,] Maze1 =
            {
                { DownRight, RightLeft, DownLeft, DownRight, RightLeft, OnlyLeft },
                { UpDown, DownRight, UpLeft, UpRight, RightLeft, DownLeft },
                { UpDown, UpRight, DownLeft, DownRight, RightLeft, UpDownLeft },
                { UpDown, OnlyRight, UpRightLeft, UpLeft, OnlyRight, UpDownLeft },
                { UpDownRight, RightLeft, DownLeft, DownRight, OnlyLeft, UpDown },
                { UpRight, OnlyLeft, UpRight, UpLeft, OnlyRight, UpLeft }
            };

            foreach (var VARIABLE in Maze1)
            {
                VARIABLE.Marked = false;
            }
            
            var _visitedPoints = new Queue<Point>();
            var _path = new List<string>();

            if (SquareLocation.X == -1 || TriangleLocation.X == -1)
                return @"Something is wrong.";

            _targetMaze = Maze1;
            _visitedPoints.Enqueue(SquareLocation);

            while (_visitedPoints.Count != 0)
            {
                var cell = _visitedPoints.Dequeue();
                _targetMaze[cell.X, cell.Y].Marked = true;
                
                if (cell.X == TriangleLocation.X && cell.Y == TriangleLocation.Y)
                    return string.Join(", ", _path);

                //move right (this is y+1)
                if (cell.Y + 1 < 6 && !_targetMaze[cell.X, cell.Y + 1].Marked && _targetMaze[cell.X, cell.Y].Right)
                {
                    _visitedPoints.Enqueue(new Point(cell.X, cell.Y + 1));
                    _path.Add("right");
                }
                
                //move down (this is (x+1)
                if (cell.X + 1 < 6 && !_targetMaze[cell.X + 1, cell.Y].Marked && _targetMaze[cell.X, cell.Y].Down)
                {
                    _visitedPoints.Enqueue(new Point(cell.X + 1, cell.Y));
                    _path.Add("down");
                }
                
                //move left (this is y-1)
                if (cell.Y - 1 >= 0 && !_targetMaze[cell.X, cell.Y - 1].Marked && _targetMaze[cell.X, cell.Y].Left)
                {
                    _visitedPoints.Enqueue(new Point(cell.X, cell.Y - 1));
                    _path.Add("left");
                }
                
                //move up (this is x-1)
                if (cell.X - 1 >= 0 && !_targetMaze[cell.X - 1, cell.Y].Marked && _targetMaze[cell.X, cell.Y].Up)
                {
                    _visitedPoints.Enqueue(new Point(cell.X - 1, cell.Y));
                    _path.Add("up");
                }
            }

            return @"No path found";
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
            TriangleLocation = new Point(--x, --y);
        }

        public void SetSquare(int x, int y)
        {
            SquareLocation = new Point(--x, --y);
        }
    }
}