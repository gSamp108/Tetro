using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetro.AgeOfTetro
{
    public struct Position
    {
        private double x;
        private double y;

        public int X { get { return (int)Math.Floor(this.x); } }
        public int Y { get { return (int)Math.Floor(this.y); } }

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Position(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public override bool Equals(object obj)
        {
            return obj is Position && ((Position)obj).X == this.X && ((Position)obj).Y == this.Y;
        }
        public override int GetHashCode()
        {
            var hash = 31d;
            unchecked
            {
                hash *= this.X * 27;
                hash *= this.Y * 27;
            }
            return hash.GetHashCode();
        }

        public Position MoveFraction(double x, double y)
        {
            return new Position(this.x + x, this.y + y);
        }
        public Position Move(int x, int y)
        {
            return new Position(this.X + x, this.Y + y);
        }

        public override string ToString()
        {
            return "(" + this.X.ToString() + ", " + this.Y.ToString() + ")";
        }
        public IEnumerable<Position> Adjacent
        {
            get
            {
                yield return new Position(this.X + 1, this.Y + 1);
                yield return new Position(this.X + 1, this.Y - 1);
                yield return new Position(this.X - 1, this.Y + 1);
                yield return new Position(this.X - 1, this.Y - 1);
            }
        }
        public IEnumerable<Position> Nearby
        {
            get
            {
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x != 0 || y != 0) yield return new Position(this.X + x, this.Y + y);
                    }
                }
            }
        }
        public IEnumerable<Position> Inrange(int distance)
        {
            for (int x = -distance; x <= distance; x++)
            {
                for (int y = -distance; y <= distance; y++)
                {
                    var scanPosition = new Position(this.X + x, this.Y + y);
                    if (this.Distance(scanPosition) <= distance) yield return scanPosition;
                }
            }
        }
        public double Distance(Position position)
        {
            return Math.Sqrt(Math.Pow(((double)position.X - (double)this.X), 2) + Math.Pow(((double)position.Y - (double)this.Y), 2));
        }
    }
}
