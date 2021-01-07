using System;
using System.Collections.Generic;
using System.Linq;

namespace Dependency_inversion_Principle
{
    /*
     * Dependency Inversion Principle
     * Basic Idea : 
     *      High Level part of system should not depend on Low-Level part of the system directly. 
     *      Instead they should depend on some kind of abstraction.
     */

    public enum Relationship
    {
        Parent,
        Child,
        Sibling,
        Partner
    }

    public class Person
    {
        public string Name { get; set; }
    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name, Relationship relation);
    }

    //low level
    public class Relationships : IRelationshipBrowser
    {
        private List<(Person, Relationship, Person)> relations 
            = new List<(Person, Relationship, Person)>();

        public void AddParentChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOf(string name, Relationship relationship)
        {   
            return relations.Where(x => x.Item1.Name == name && x.Item2 == relationship).Select(r => r.Item3);
        }
    }
    
    public class Program
    {
        Program(IRelationshipBrowser browser)
        {
            foreach(var p in browser.FindAllChildrenOf("John", Relationship.Parent))
            {   
                Console.WriteLine($"John has a child called {p.Name}");
            }
        }
        
        static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Child1" };
            var child2 = new Person { Name = "Child2" };

            var relationships = new Relationships();
            relationships.AddParentChild(parent, child1);
            relationships.AddParentChild(parent, child2);

            new Program(relationships);
        }
    }
}
