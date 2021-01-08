using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace PrototypeDesign
{
    // public interface IPrototype<T>{
    //     T DeepCopy();
    // }
    public static class ExtensionMethod{
        public static T DeepCopy<T>(this T self){
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0,SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T) copy;
        }
    }
    [Serializable]
    public class Person //: IPrototype<Person>
    {
        public string[] Names;
        public Address Address;

        public Person(string[] name, Address address){
            Names = name;
            Address = address;
        }
        
        // public Person(Person other){
        //     Names = other.Names;
        //     Address = new Address(other.Address);
        // }

        public override string ToString()
        {
            return $"Name : {string.Join(" ", Names)} \n Address : {Address}";
        }

        // public Person DeepCopy(){
        //     return new Person(Names, Address.DeepCopy());
        // }
    }

    [Serializable]
    public class Address //: IPrototype<Address>
    {
        public string StreetName;
        public int HouseNumber;

        public Address(string streetName, int number){
            StreetName = streetName;
            HouseNumber = number;
        }

        public override string ToString()
        {
            return $"Streetname : {StreetName} \n House Number : {HouseNumber}";
        }
        // public Address DeepCopy(){
        //     return new Address(StreetName, HouseNumber);
        // }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var john = new Person(
            new string[]{"john", "Smith"},
            new Address("Londan Road", 123)
            );

            var jane = john.DeepCopy();
            jane.Names[0] = "jane";
            jane.Address.HouseNumber = 321;

            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}
