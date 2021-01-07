using System;
using System.Collections.Generic;

namespace Open_Closed_Principle
{
    /* Open Closed Principle states */
    /* that part of system or subsystem have to be open for extension so you could be able to extend functionality
     * But it should be closed from modification
     */

    /* 
     * In this example, ProductFilter Class is used to filter products in product Array.
     * We need to add conditions in ProductFilter in order to filter product.
     * But conditions may change as it depends on client requirements.
     * So we can add new functionality to filter the product based on color, size or both
     * so there is no need to modify PrdouctFilter Class
     */

    /* Here ProductFilter is open for extension with the help of interfaces.
     * But there is no need to modify that class, instead we can create new class that inherit ISpecification interface.
     * We can specify conodition in those classes.
     */

    #region Product Details
    public enum Color
    {
        Red, Green, Blue
    }
    public enum Size
    {
        Small, Medium, Large
    }
    public class Product
    {
        private string name;
        private Color color;
        private Size size;

        public string ProductName { get => name; }
        public Color ProductColor { get => color; }
        public Size ProductSize { get => size; }

        public Product(String name, Color color, Size size)
        {
            if(name == null)
            {

            }
            this.name = name;
            this.color = color;
            this.size = size;
        }
    }
    #endregion

    #region Interfaces
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }
    #endregion

    #region Specification Filter

    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }

        public bool IsSatisfied(Product t)
        {
            return t.ProductColor == color;
        }
    }
    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;

        public SizeSpecification(Size size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Product t)
        {
            return t.ProductSize == size;
        }
    }

    public class AndSpecificatoin<T> : ISpecification<T>
    {
        ISpecification<T> first, second;

        public AndSpecificatoin(ISpecification<T> first, ISpecification<T> second)
        {
            this.first = first;
            this.second = second;
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    public class ProductFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
                if (spec.IsSatisfied(i))
                    yield return i;
        }
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Red, Size.Large);
            var toy = new Product("Toy", Color.Red, Size.Small);

            Product[] products = { apple, tree, house, toy };

            var pf = new ProductFilter();
            Console.WriteLine("Filterd Products : ");
            foreach(var p in pf.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($" - {p.ProductName} is green");
            }

            Console.WriteLine("Filterd large Green Products : ");
            foreach (var p in pf.Filter(products, new AndSpecificatoin<Product>(
                new ColorSpecification(Color.Green), new SizeSpecification(Size.Large))))
            {
                Console.WriteLine($" - {p.ProductName} is large green");
            }
        }
    }
}
