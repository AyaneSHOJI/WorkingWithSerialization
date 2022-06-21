using System.Xml;
using System.Xml.Serialization;
using Packt.Shared;
using static System.Console;
using static System.Environment;
using static System.IO.Path;
using Newtonsoft;
using NewJson = System.Text.Json.JsonSerializer;
using System.Text.Json;
using System.Text.Json.Serialization;


// Errata : https://github.com/markjprice/cs10dotnet6/blob/main/errata.md#page-402---controlling-json-processing
//Book csharp10 = new(title: "C# 10 and .NET 6 - Modern Cross-plateform Developement")
//{
//    Author = "Mark J Price",
//    PublishDate = new(year: 2021, month: 11, day: 9),
//    Pages = 823,
//    Created = DateTimeOffset.UtcNow
//};

//JsonSerializerOptions options = new()
//{
//    IncludeFields = true,
//    PropertyNameCaseInsensitive = true,
//    WriteIndented = true,
//    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
//};

//string filePath = Combine(CurrentDirectory, "book.json");

//using (Stream fileStream = File.Create(filePath))
//{
//    JsonSerializer.Serialize<Book>(utf8Json: fileStream, value: csharp10, options);
//}

//WriteLine("Written {0:N0} bytes of JSON to {1}",
//    arg0: new FileInfo(filePath).Length,
//    arg1: filePath);
//WriteLine();

//// Display the serialized object graph
//WriteLine(File.ReadAllText(filePath));
//public class Book
//{
//    // constructor to set non-nummable property
//    public Book(string title)
//    {
//        Title = title;
//    }

//    // properties

//    public string Title { get; set; }
//    public string? Author { get; set; }

//    // fields
//    [JsonInclude]
//    public DateOnly PublishDate;
//    // System.NotSupportedException: 'Serialization and deserialization of 'System.DateOnly' instances are not supported. Path: $.PublishDate.'

//    [JsonInclude]
//    public DateTimeOffset Created;

//    public ushort Pages;
//}

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

//using (StreamWriter jsonStrem = File.CreateText(jsonPath))
//{
//    Newtonsoft.Json.JsonSerializer jss = new();

//    // serialize the object graph into a string
//    jss.Serialize(jsonStrem, people);
//}

//WriteLine();
//WriteLine("Written {0:N0} bytes of JSON to: {1}",
//    arg0: new FileInfo(jsonPath).Length,
//    arg1: jsonPath);

//// display the serialize object graph
//WriteLine(File.ReadAllText(jsonPath)); 


using (FileStream jsonLoad = File.Open(jsonPath, FileMode.Open))
{
    // deserialize object graph into a list of Person
    List<Person>? loadedPeople = await NewJson.DeserializeAsync(utf8Json: jsonLoad, returnType: typeof(List<Person>)) as List<Person>;

    if (loadedPeople != null)
    {
        foreach (Person p in loadedPeople)
        {
            WriteLine("{0} has {1} children.",
                p.LastName, p.Children?.Count ?? 0);
        }
    }
}


// create object that will format a list of Persons as XML
XmlSerializer xs = new(people.GetType());

// create a file to write to
string path = Combine(CurrentDirectory, "people.xml");

XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();

using (FileStream stream = File.Create(path))
{
    // serialize the object graph to the stream
    xs.Serialize(stream, people);

}

WriteLine("Written {0:N0} bytes of XML to {1}",
    arg0: new FileInfo(path).Length,
    arg1: path);
WriteLine();

// Display the serialized object graph
WriteLine(File.ReadAllText(path));

using (FileStream xmlLoad = File.Open(path, FileMode.Open))
{
    // deserialize ad cas the object graph into a List of Person
    List<Person>? loadedPeople = xs.Deserialize(xmlLoad) as List<Person>;

    if (loadedPeople != null)
    {
        foreach (Person p in loadedPeople)
        {
            WriteLine("{0} has {1} children", p.LastName, p.Children?.Count ?? 0);
        }
    }
}
