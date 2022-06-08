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
        public class Block
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
        
        private static readonly Block[,] Maze1 =
        {
            { DownRight(), RightLeft(), DownLeft(), DownRight(), RightLeft(), OnlyLeft() },
            { UpDown(), DownRight(), UpLeft(), UpRight(), RightLeft(), DownLeft() },
            { UpDown(), UpRight(), DownLeft(), DownRight(), RightLeft(), UpDownLeft() },
            { UpDown(), OnlyRight(), UpRightLeft(), UpLeft(), OnlyRight(), UpDownLeft() },
            { UpDownRight(), RightLeft(), DownLeft(), DownRight(), OnlyLeft(), UpDown() },
            { UpRight(), OnlyLeft(), UpRight(), UpLeft(), OnlyRight(), UpLeft() }
        };
        
        private static readonly Block[,] Maze2 =
        {
            { OnlyRight(), DownRightLeft(), OnlyLeft(), DownRight(), DownRightLeft(), OnlyLeft() },
            { DownRight(), UpLeft(), DownRight(), UpLeft(), UpRight(), DownLeft() },
            { UpDown(), DownRight(), UpLeft(), DownRight(), RightLeft(), UpDownLeft() },
            { UpDownRight(), UpLeft(), DownRight(), UpLeft(), OnlyDown(), UpDown() },
            { UpDown(), OnlyDown(), UpDown(), DownRight(), UpLeft(), UpDown() },
            { OnlyUp(), UpRight(), UpLeft(), UpRight(), RightLeft(), UpLeft() }
        };
        
        private static readonly Block[,] Maze3 =
        {
            { DownRight(), RightLeft(), DownLeft(), OnlyDown(), DownRight(), DownLeft() },
            { OnlyUp(), OnlyDown(), UpDown(), UpRight(), UpLeft(), UpDown() },
            { DownRight(), UpDownLeft(), UpDown(), DownRight(), DownLeft(), UpDown() },
            { UpDown(), UpDown(), UpDown(), UpDown(), UpDown(), UpDown() },
            { UpDown(), UpRight(), UpLeft(), UpDown(), UpDown(), UpDown() },
            { UpRight(), RightLeft(), RightLeft(), UpLeft(), UpRight(), UpLeft() }
        };
        
        private static readonly Block[,] Maze4 =
        {
            { DownRight(), DownLeft(), OnlyRight(), RightLeft(), RightLeft(), DownLeft() },
            { UpDown(), UpDown(), DownRight(), RightLeft(), RightLeft(), UpDownLeft() },
            { UpDown(), UpRight(), UpLeft(), DownRight(), OnlyLeft(), UpDown() },
            { UpDown(), OnlyRight(), RightLeft(), UpRightLeft(), RightLeft(), UpDownLeft() },
            { UpDownRight(), RightLeft(), RightLeft(), RightLeft(), DownLeft(), UpDown() },
            { UpRight(), RightLeft(), OnlyLeft(), OnlyRight(), UpLeft(), OnlyUp() }
        };
        
        private static readonly Block[,] Maze5 =
        {
            { OnlyRight(), RightLeft(), RightLeft(), RightLeft(), DownRightLeft(), DownLeft() },
            { DownRight(), RightLeft(), RightLeft(), DownRightLeft(), UpLeft(), OnlyUp() },
            { UpDownRight(), DownLeft(), OnlyRight(), UpLeft(), DownRight(), DownLeft() },
            { UpDown(), UpRight(), RightLeft(), DownLeft(), OnlyUp(), UpDown() },
            { UpDown(), DownRight(), RightLeft(), UpRightLeft(), OnlyLeft(), UpDown() },
            { OnlyUp(), UpRight(), RightLeft(), RightLeft(), RightLeft(), UpLeft() }
        };

        private static readonly Block[,] Maze6 =
        {
            { OnlyDown(), DownRight(), DownLeft(), OnlyRight(), DownRightLeft(), DownLeft() },
            { UpDown(), UpDown(), UpDown(), DownRight(), UpLeft(), UpDown() },
            { UpDownRight(), UpLeft(), OnlyUp(), UpDown(), DownRight(), UpLeft() },
            { UpRight(), DownLeft(), DownRight(), UpDownLeft(), UpDown(), OnlyDown() },
            { DownRight(), UpLeft(), OnlyUp(), UpDown(), UpRight(), UpDownLeft() },
            { UpRight(), RightLeft(), RightLeft(), UpLeft(), OnlyRight(), UpLeft() }
        };
        
        private static readonly Block[,] Maze7 =
        {
            { DownRight(), RightLeft(), RightLeft(), DownLeft(), DownRight(), DownLeft() },
            { UpDown(), DownRight(), OnlyLeft(), UpRight(), UpLeft(), UpDown() },
            { UpRight(), UpLeft(), DownRight(), OnlyLeft(), DownRight(), UpLeft() },
            { DownRight(), DownLeft(), UpDownRight(), RightLeft(), UpLeft(), OnlyDown() },
            { UpDown(), OnlyUp(), UpRight(), RightLeft(), DownLeft(), UpDown() },
            { UpRight(), RightLeft(), RightLeft(), RightLeft(), UpRightLeft(), UpLeft() }
        };
        
        private static readonly Block[,] Maze8 =
        {
            { OnlyDown(), DownRight(), RightLeft(), DownLeft(), DownRight(), DownLeft() },
            { UpDownRight(), UpRightLeft(), OnlyLeft(), UpRight(), UpLeft(), UpDown() },
            { UpDown(), DownRight(), RightLeft(), RightLeft(), DownLeft(), UpDown() },
            { UpDown(), UpRight(), DownLeft(), OnlyRight(), UpRightLeft(), UpLeft() },
            { UpDown(), OnlyDown(), UpRight(), RightLeft(), RightLeft(), OnlyLeft() },
            { UpRight(), UpRightLeft(), RightLeft(), RightLeft(), RightLeft(), OnlyLeft() }
        };
        
        private static readonly Block[,] Maze9 =
        {
            { OnlyDown(), DownRight(), RightLeft(), RightLeft(), DownRightLeft(), DownLeft() },
            { UpDown(), UpDown(), DownRight(), OnlyLeft(), UpDown(), UpDown() },
            { UpDownRight(), UpRightLeft(), UpLeft(), DownRight(), UpLeft(), UpDown() },
            { UpDown(), OnlyDown(), DownRight(), UpLeft(), OnlyRight(), UpDownLeft() },
            { UpDown(), UpDown(), UpDown(), DownRight(), DownLeft(), OnlyUp() },
            { UpRight(), UpLeft(), UpRight(), UpLeft(), UpRight(), OnlyLeft() }
        };

        private static readonly Dictionary<Point, Block[,]> MazeIdentifierDict = new Dictionary<Point, Block[,]>
        {
            { new Point(2, 1), Maze1 },
            { new Point(3, 6), Maze1 },
            { new Point(4, 2), Maze2 },
            { new Point(2, 5), Maze2 },
            { new Point(4, 4), Maze3 },
            { new Point(4, 6), Maze3 },
            { new Point(1, 1), Maze4 },
            { new Point(4, 1), Maze4 },
            { new Point(6, 4), Maze5 },
            { new Point(3, 5), Maze5 },
            { new Point(1, 5), Maze6 },
            { new Point(5, 3), Maze6 },
            { new Point(1, 2), Maze7 },
            { new Point(6, 2), Maze7 },
            { new Point(1, 4), Maze8 },
            { new Point(4, 3), Maze8 },
            { new Point(2, 3), Maze9 },
            { new Point(5, 1), Maze9 }
        };

        public Node SquareLocation { get; private set; }
        public Node TriangleLocation { get; private set; }
        public Block[,] TargetMaze { get; private set; }

        public Maze(Bomb bomb) : base(bomb)
        {
            TriangleLocation = new Node(-1, -1);
            SquareLocation = new Node(-1, -1);
            TargetMaze = null;
        }

        public override string Solve()
        {
            foreach (var block in TargetMaze)
                block.Marked = false;
            
            var visitedPoints = new Queue<Node>();

            if (SquareLocation.X == -1 || TriangleLocation.X == -1)
                return @"Something is wrong.";
            
            visitedPoints.Enqueue(SquareLocation);

            while (visitedPoints.Count != 0)
            {
                var cell = visitedPoints.Dequeue();
                TargetMaze[cell.X, cell.Y].Marked = true;
                
                if (cell.X == TriangleLocation.X && cell.Y == TriangleLocation.Y)
                    return ConstructPath(cell);

                //move right (this is y+1)
                if (cell.Y + 1 < 6 && !TargetMaze[cell.X, cell.Y + 1].Marked && TargetMaze[cell.X, cell.Y].Right)
                    visitedPoints.Enqueue(new Node(cell.X, cell.Y + 1) {Previous = cell});

                //move left (this is y-1)
                if (cell.Y - 1 >= 0 && !TargetMaze[cell.X, cell.Y - 1].Marked && TargetMaze[cell.X, cell.Y].Left)
                    visitedPoints.Enqueue(new Node(cell.X, cell.Y - 1) {Previous = cell});

                //move down (this is (x+1)
                if (cell.X + 1 < 6 && !TargetMaze[cell.X + 1, cell.Y].Marked && TargetMaze[cell.X, cell.Y].Down)
                    visitedPoints.Enqueue(new Node(cell.X + 1, cell.Y) {Previous = cell});
                
                //move up (this is x-1)
                if (cell.X - 1 >= 0 && !TargetMaze[cell.X - 1, cell.Y].Marked && TargetMaze[cell.X, cell.Y].Up)
                    visitedPoints.Enqueue(new Node(cell.X - 1, cell.Y) {Previous = cell});
            }

            return @"No path found";
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

            var pathBuilder = new StringBuilder();
            path.Reverse();
            for (int i = 1; i < path.Count; i++)
            {
                if (path[i].X - path[i - 1].X == -1)
                    pathBuilder.Append("Up. ");
                
                if (path[i].X - path[i - 1].X == 1)
                    pathBuilder.Append("Down. ");
                
                if (path[i].Y - path[i - 1].Y == -1)
                    pathBuilder.Append("Left. ");
                
                if (path[i].Y - path[i - 1].Y == 1)
                    pathBuilder.Append("Right. ");
            }

            return pathBuilder.ToString();
        }

        public bool AssignCircle(int x, int y)
        {
            try
            {
                TargetMaze = MazeIdentifierDict[new Point(x, y)];
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
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