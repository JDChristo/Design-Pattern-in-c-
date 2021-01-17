using System;
using Autofac;

namespace _2_Bridge_Patterns
{
    public interface IRenderer{
        void RenderCircle(float radius);
    }

    public class VectorRenderer : IRenderer{
        public void RenderCircle(float radius){
            Console.WriteLine($"Drawing a circle of raidus {radius}");
        }
    }

    public class RasterRenderer : IRenderer{
        public void RenderCircle(float radius){
            Console.WriteLine($"Drawing a pixels for circle of raidus {radius}");
        }
    }

    public abstract class Shape
    {
        protected IRenderer renderer;

        protected Shape(IRenderer renderer){
            this.renderer = renderer;
        }

        public abstract void Draw();
        public abstract void Resize(float factor);
    }

    public class Circle : Shape{
        float radius;
        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            this.radius = radius;
        }

        public override void Draw()
        {
            renderer.RenderCircle(radius);
        }

        public override void Resize(float factor)
        {
            radius *= factor;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IRenderer renderer = new VectorRenderer();
            //IRenderer renderer = new RasterRenderer();
            var circle = new Circle(renderer, 5f);
            
            circle.Draw();
            circle.Resize(2f);
            circle.Draw();
        }
    }
}
