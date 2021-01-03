using System;
using System.Text;
using System.IO;



namespace Liskov_Substitution_Principle
{
    /* ---------------Liskov Substitution Principle----------
     * ideas is to substitute a base type for sub-type
     * */

    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle() { }
        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public override string ToString()
        {
            return $"{nameof(Width)} : {Width}, {nameof(Height)} : {Height}";
        }
    }

    public class Square : Rectangle
    {
        public override int Width { get => base.Width; set => base.Width = base.Height = value; }
        public override int Height { get => base.Height; set => base.Width = base.Height = value; }
    }

    class Program
    {
        static public int Area(Rectangle r) => r.Width * r.Height;
        static void Main(string[] args)
        {
            Rectangle rect = new Rectangle(2,3);
            Console.WriteLine($"{rect} has area of {Area(rect)}");

            Square sq = new Square();
            sq.Width = 12;
            Console.WriteLine($"{sq} has area of {Area(sq)}");
        }
    }
}
