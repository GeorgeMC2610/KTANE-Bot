using System;

namespace KTANE_Bot
{
    public class Maze : KTANE_Module
    {
        private int[] _firstCircleCoordinates;
        private int[] _secondCircleCoordinates;

        private static int[,] _firstMaze = new int[,]
        {
            { 0, 1, 0, 1, 0, 2, 0, 1, 0, 1, 0 },
        };

        public Maze(Bomb bomb) : base(bomb)
        {
            _firstCircleCoordinates = new int[2];
            _secondCircleCoordinates = new int[2];
        }

        public override string Solve()
        {
            return "";
        }

        public void AssignFirstCircle(int x, int y)
        {
            _firstCircleCoordinates[0] = x;
            _firstCircleCoordinates[1] = y;
        }

        public void AssignSecondCircle(int x, int y)
        {
            _secondCircleCoordinates[0] = x;
            _secondCircleCoordinates[0] = y;
        }
    }
}