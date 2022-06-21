using System.Xml;
using System.Xml.Serialization;
using Packt.Shared;

using static System.Console;
using static System.Environment;
using static System.IO.Path;
using Newtonsoft;



// create an object graph
List<Person> people = new()
{
    new(30000M)
    {
        FirstName = "Alice",
        LastName = "Smith",
        DateOfBirth = new(1974, 3, 14)
    },
    new(40000M)
    {
        FirstName = "Bob",
        LastName = "Jones",
        DateOfBirth = new(1969, 11, 23)
    },
    new(20000M)
    {
        FirstName = "Charlie",
        LastName = "Cox",
        DateOfBirth = new(1984, 5, 4),
        Children = new()
        {
            new(0M)
            {
                FirstName = "Sally",
                LastName = "Cox",
                DateOfBirth = new(2000, 7, 12)
            }
        }
    }
};

// create a file to write to
string jsonPath = Combine(CurrentDirectory, "people.json");

using (StreamWriter jsonStrem = File.CreateText(jsonPath))
{
    Newtonsoft.Json.JsonSerializer jss = new();

    // serialize the object graph into a string
    jss.Serialize(jsonStrem, people);
}

WriteLine();
WriteLine("Written {0:N0} bytes of JSON to: {1}",
    arg0: new FileInfo(jsonPath).Length,
    arg1: jsonPath);


// display the serialize object graph
WriteLine(File.ReadAllText(jsonPath)); 
//// create object that will format a list of Persons as XML
//XmlSerializer xs = new(people.GetType());

//// create a file to write to
//string path = Combine(CurrentDirectory, "people.xml");

//XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();

//using (FileStream stream = File.Create(path))
//{
//    // serialize the object graph to the stream
//    xs.Serialize(stream, people);

//}

//WriteLine("Written {0:N0} bytes of XML to {1}",
//    arg0: new FileInfo(path).Length,
//    arg1: path);
//WriteLine();

//// Display the serialized object graph
//WriteLine(File.ReadAllText(path));

//using (FileStream xmlLoad = File.Open(path, FileMode.Open))
//{
//    // deserialize ad cas the object graph into a List of Person
//    List<Person>? loadedPeople = xs.Deserialize(xmlLoad) as List<Person>;

//    if(loadedPeople != null)
//    {
//        foreach(Person p in loadedPeople)
//        {
//            WriteLine("{0} has {1} children", p.LastName, p.Children?.Count ?? 0);  
//        }
//    }
//}
