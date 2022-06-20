namespace Packt.Shared;
using System.Xml.Serialization;

public class Person
{
    public Person(decimal initialSalary)
    {
        Salary = initialSalary;
    }
    [XmlAttribute("fname")]
    public string? FirstName { get; set; }
    [XmlAttribute("lname")]
    public string? LastName { get; set; }
    [XmlAttribute("dob")]
    public DateTime DateOfBirth;
    public HashSet<Person>? Children { get; set; }
    public decimal? Salary { get; set; }

    public Person() { }
}

