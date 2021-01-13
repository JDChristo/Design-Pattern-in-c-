using Autofac;
using MoreLinq;
using NUnit.Framework;
//using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SingletonPattern
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> cities;

        private static int instanceCount;
        public static int InstanceCount => instanceCount;

        private SingletonDatabase()
        {
            instanceCount++;
            Console.WriteLine("Initializing Database");

            cities = File.ReadAllLines(
                Path.Combine(
                    new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName,
                    "capitals.txt")
                )
                .Batch(2)
                .ToDictionary(
                 x => x.ElementAt(0).Trim(),
                 x => int.Parse(x.ElementAt(1))
                );
        }

        public int GetPopulation(string city)
        {
            return cities[city];
        }

        private static Lazy<SingletonDatabase> instance =
            new Lazy<SingletonDatabase>(() => new SingletonDatabase());
        public static SingletonDatabase Instance => instance.Value;
    }

    public class OrdinaryDatabase : IDatabase
    {
        private Dictionary<string, int> cities;

        private OrdinaryDatabase()
        {
            Console.WriteLine("Initializing Database");

            cities = File.ReadAllLines(
                Path.Combine(
                    new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName,
                    "capitals.txt")
                )
                .Batch(2)
                .ToDictionary(
                 x => x.ElementAt(0).Trim(),
                 x => int.Parse(x.ElementAt(1))
                );
        }

        public int GetPopulation(string city)
        {
            return cities[city];
        }
    }

    public class SingletonRecordFinder
    {
        public int Totalpopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
            {
                result += SingletonDatabase.Instance.GetPopulation(name);
            }
            return result;
        }
    }

    public class ConfigurableRecordFinder {
        private IDatabase database;
        public ConfigurableRecordFinder(IDatabase db)
        {
            database = db;
        }
        public int Totalpopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
            {
                result += database.GetPopulation(name);
            }
            return result;
        }

    }

    public class DummyDatabase : IDatabase
    {
        public int GetPopulation(string name)
        {
            return new Dictionary<string, int> 
            { 
                ["alpha"] = 1,
                ["beta"] = 2,
                ["gamma"] = 3
            }[name];
        }
    }

    [TestFixture]
    public class SingletonTest
    {
        [Test]
        public void IsSingletonTest()
        {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;
            Assert.That(db, Is.SameAs(db2));
            Assert.That(SingletonDatabase.InstanceCount, Is.EqualTo(1));
        }

        [Test]
        public void SingletonPopulationTest()
        {
            var rf = new SingletonRecordFinder();
            var names = new[] { "New York", "Tokyo" };

            int tp = rf.Totalpopulation(names);
            Assert.That(tp, Is.EqualTo(1230000 + 2234500));
        }

        [Test]
        public void ConfigurablePopulationTest()
        {
            var rf = new ConfigurableRecordFinder( new DummyDatabase());
            var names = new[] { "alpha", "gamma" };

            int tp = rf.Totalpopulation(names);
            Assert.That(tp, Is.EqualTo(4));
        }

        [Test]
        public void DIPoplationTest()
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<OrdinaryDatabase>()
                .As<IDatabase>()
                .SingleInstance();
            cb.RegisterType<ConfigurableRecordFinder>();

            using(var c = cb.Build())
            {
                var rf = c.Resolve<ConfigurableRecordFinder>();
            }

        }
    }

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        var db = SingletonDatabase.Instance;
    //        var city = "Tokyo";

    //        Console.Write($"{city} has population {db.GetPopulation(city)}");
    //    }
    //}
}
