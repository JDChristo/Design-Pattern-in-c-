using System;

namespace FacetedBuilder
{
    public class Person
    {
        public string StreetAddress, PostCode, City;
        public string CompanyName, Designation;
        public int AnnualIncome;

        public override string ToString()
        {
            return 
                $"{nameof(StreetAddress)} : {StreetAddress}, " +
                $"{ nameof(PostCode)} : { PostCode}, " +
                $"{ nameof(City)} : { City}, " +
                $"\n{ nameof(CompanyName)} : { CompanyName}" +
                $"{ nameof(Designation)} : { Designation}," +
                $"{ nameof(AnnualIncome)} : { AnnualIncome}";
        }
    }

    public class PersonBuilder
    {
        protected Person person = new Person();

        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);
        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }

    }
    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAddressBuilder At(string StreetAddress)
        {
            person.StreetAddress = StreetAddress;
            return this;
        }
        public PersonAddressBuilder PostCode(string PostCode)
        {
            person.PostCode = PostCode;
            return this;
        }
        public PersonAddressBuilder City(string City)
        {
            person.City = City;
            return this;
        }
    }
    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder As(string position)
        {
            person.Designation = position;
            return this;
        }

        public PersonJobBuilder Earns(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            Person person = pb
                .Lives.At("Adres")
                      .City("CountryCity")
                      .PostCode("253679")
                .Works.At("Company")
                      .As("Developer")
                      .Earns(100);

            Console.WriteLine(person);
        }
    }
}
