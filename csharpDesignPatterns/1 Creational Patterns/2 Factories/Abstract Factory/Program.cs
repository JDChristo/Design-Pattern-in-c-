using System;
using System.Collections.Generic;

namespace AbstractFacatoryMethod
{
    public interface IHotDrink
    {
        void Consume();
    }

    public interface IHotDrinkFactory
    {
        IHotDrink MakeDrink(float amount);
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Consumed Tea ");
        }
    }
    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Consumed Coffee ");
        }
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink MakeDrink(float amount)
        {
            Console.WriteLine($"Preparing Tea, Amount : {amount} ");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink MakeDrink(float amount)
        {
            Console.WriteLine($"Preparing Coffee, Amount : {amount} ");
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
       
        private List<Tuple<string, IHotDrinkFactory>> factories = 
            new List<Tuple<string, IHotDrinkFactory>>();
        public HotDrinkMachine()
        {
            foreach(var t in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if(typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
                {
                    factories.Add(Tuple.Create(
                        t.Name.Replace("Factory", string.Empty),
                        (IHotDrinkFactory)Activator.CreateInstance(t)
                        ));
                }
            }
        }

        public IHotDrink Prepare()
        {
            Console.WriteLine("Aviable drinks: ");
            for(var i = 0; i < factories.Count; i++)
            {
                var t = factories[i];
                Console.WriteLine($"{i}. {t.Item1}");
            }

            while (true)
            {
                Console.WriteLine("Specify drink: ");
                string s;
                if((s = Console.ReadLine()) != null
                    && int.TryParse(s,out int i)
                    && i >= 0
                    && i < factories.Count)
                {
                    Console.WriteLine("Specify amount: ");
                    s = Console.ReadLine();
                    if(s != null && int.TryParse(s,out int amount) && amount > 0)
                    {
                        return factories[i].Item2.MakeDrink(amount);
                    }
                }

                Console.WriteLine(" Incorrect input ");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();
            var drink = machine.Prepare();
            drink.Consume();
        }
    }
}
