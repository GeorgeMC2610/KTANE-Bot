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

            public static Block Nothing() => new Block(false, false, false, false);
            public static Block OnlyLeft() => new Block(false, false, false, true);
            
            public static Block OnlyRight() => new Block(false, false, true, false);
            public static Block RightLeft() => new Block(false, false, true, true);

            public static Block OnlyDown() => new Block(false, true, false, false);
            public static Block DownLeft() => new Block(false, true, false, true);
            public static Block DownRight() => new Block(false, true, true, false);
            public static Block DownRightLeft() => new Block(false, true, true, true);

            public static Block OnlyUp() => new Block(true, false, false, false);
            public static Block UpLeft() => new Block(true, false, false, true);
            public static Block UpRight() => new Block(true, false, true, false);
            public static Block UpRightLeft() => new Block(true, false, true, true);
            public static Block UpDown() => new Block(true, true, false, false);
            public static Block UpDownLeft() => new Block(true, true, false, true);
            public static Block UpDownRight() => new Block(true, true, true, false);
            public static Block All() => new Block(true, true, true, true);
        }

        public class Node
        {
            private Point _location;

            public int X
            {
                get => _location.X;

                set => _location = new Point(value, _location.Y);
            }
            
            public int Y
            {
                get => _location.Y;

                set => _location = new Point(_location.X, value);
            }

            public Node Previous { get; set; }

            public Node(int x, int y)
            {
                _location = new Point(x, y);
                Previous = null;
            }
        }
        
        public Node SquareLocation { get; private set; }
        public Node TriangleLocation { get; private set; }
        private int[] _circles;
        private Block[,] _targetMaze;

        public Maze(Bomb bomb) : base(bomb)
        {
            _circles = new[] {-1, -1, -1, -1};
            TriangleLocation = new Node(-1, -1);
            SquareLocation = new Node(-1, -1);
        }

        public override string Solve()
        {
            Block[,] Maze1 =
            {
                { DownRight(), RightLeft(), DownLeft(), DownRight(), RightLeft(), OnlyLeft() },
                { UpDown(), DownRight(), UpLeft(), UpRight(), RightLeft(), DownLeft() },
                { UpDown(), UpRight(), DownLeft(), DownRight(), RightLeft(), UpDownLeft() },
                { UpDown(), OnlyRight(), UpRightLeft(), UpLeft(), OnlyRight(), UpDownLeft() },
                { UpDownRight(), RightLeft(), DownLeft(), DownRight(), OnlyLeft(), UpDown() },
                { UpRight(), OnlyLeft(), UpRight(), UpLeft(), OnlyRight(), UpLeft() }
            };

            foreach (var VARIABLE in Maze1)
            {
                VARIABLE.Marked = false;
            }
            
            var _visitedPoints = new Queue<Node>();
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
                    return ConstructPath(cell);

                //move right (this is y+1)
                if (cell.Y + 1 < 6 && !_targetMaze[cell.X, cell.Y + 1].Marked && _targetMaze[cell.X, cell.Y].Right)
                {
                    _visitedPoints.Enqueue(new Node(cell.X, cell.Y + 1) {Previous = cell});
                }
                
                //move down (this is (x+1)
                if (cell.X + 1 < 6 && !_targetMaze[cell.X + 1, cell.Y].Marked && _targetMaze[cell.X, cell.Y].Down)
                {
                    _visitedPoints.Enqueue(new Node(cell.X + 1, cell.Y) {Previous = cell});
                }
                
                //move left (this is y-1)
                if (cell.Y - 1 >= 0 && !_targetMaze[cell.X, cell.Y - 1].Marked && _targetMaze[cell.X, cell.Y].Left)
                {
                    _visitedPoints.Enqueue(new Node(cell.X, cell.Y - 1) {Previous = cell});
                }
                
                //move up (this is x-1)
                if (cell.X - 1 >= 0 && !_targetMaze[cell.X - 1, cell.Y].Marked && _targetMaze[cell.X, cell.Y].Up)
                {
                    _visitedPoints.Enqueue(new Node(cell.X - 1, cell.Y) {Previous = cell});
                }
            }

            return @"No path found";
        }

        private string ShowMarkedNodes()
        {
            var builder = new StringBuilder();

            for (int i = 0; i < 6; i++)
            {
                builder.Append('[');
                for (int j = 0; j < 6; j++)
                {
                    builder.Append(_targetMaze[i, j].Marked ? 'X' : '.');
                }
                builder.Append("] " + Environment.NewLine);
            }

            return builder.ToString();
        }

        private string ConstructPath(Node end)
        {
            var previous = end;
            List<Node> path = new List<Node>();

            while (previous != null)
            {
                path.Add(previous);
                previous = previous.Previous;
            }

            return path.Count.ToString();
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
            TriangleLocation = new Node(--x, --y);
        }

        public void SetSquare(int x, int y)
        {
            SquareLocation = new Node(--x, --y);
        }
    }
}