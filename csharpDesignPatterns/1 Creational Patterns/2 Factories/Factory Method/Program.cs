using System;

namespace FactoryPattern
{
    
    public class Point
    {
        private double x, y;
        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"x : {x} \ny : {y}";
        }
        public static class Factory
        {
            #region-----------------------Factory Method -----------------------
            // because we cannot overload constructor with same paramters
            public static Point CartesianPoints(double x, double y) => new Point(x, y);
            public static Point PolarPoints(double x, double y) => new Point(x * Math.Cos(y), y * Math.Sin(x));
            #endregion
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var point = Point.Factory.CartesianPoints(10, 30);
            Console.WriteLine(point);
        }
    }
}
