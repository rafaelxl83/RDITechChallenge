using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge2
{
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }
    }

    public class Robot
    {
        public Robot() { }

        private void Walk(String aPath)
        {
            allPoints.Clear();
            foreach (var step in aPath)
                if (Step(step, aPath))
                    return;
        }

        private bool Step(char aDir, String fullPath)
        {
            if (allPoints.Count() == 0)
                allPoints.Add(new Point());

            Point next, last = allPoints.Last();
            switch(aDir)
            {
                case 'R':
                    next = new Point(last.X+1, last.Y);
                    break;
                case 'L':
                    next = new Point(last.X-1, last.Y);
                    break;
                case 'U':
                    next = new Point(last.X, last.Y+1);
                    break;
                case 'D':
                    next = new Point(last.X, last.Y-1);
                    break;
                default:
                    next = new Point();
                    break;
            }

            // the circle path must have at least 4 points
            // including the next one
            if (allPoints.Count() > 2)
            {
                Point test = allPoints.Find(p => p.X == next.X && p.Y == next.Y);
                if (test != null)
                {
                    // Point test = allPoints.First(p => p.X == next.X && p.Y == next.Y);
                    int start = allPoints.IndexOf(test);
                    int end = allPoints.Count();

                    circlePath = fullPath.Substring(start, end - start);
                    return true;
                }
            }

            allPoints.Add(next);
            return false;
        }

        public String getLastLoop(String aPath)
        {
            Walk(aPath);
            return circlePath;
        }

        List<Point> allPoints = new List<Point>();
        String circlePath = "";
    }
}
