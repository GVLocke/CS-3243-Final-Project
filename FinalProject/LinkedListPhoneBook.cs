namespace FinalProject;

public class LinkedListPhoneBook : IPhoneBook
{
    private readonly LinkedList<Person> _directory = [];
    public LinkedListPhoneBook(StreamReader file)
    {
        var lines = new List<string>();
        using (file)
        {
            while (file.ReadLine() is { } line)
            {
                lines.Add(line);
            }
        }

        lines.RemoveAt(0);
        foreach (var person in lines.Select(line => line.Split(',')).Select(parts => new Person(
                     firstNameLastName: parts[1] + parts[2],
                     firstName: parts[1],
                     lastName: parts[2],
                     address: parts[3],
                     city: parts[4],
                     state: parts[5],
                     zipCode: int.Parse(parts[6]),
                     email: parts[7],
                     telephoneNumber: parts[8]
                     )))
        {
            _directory.AddLast(person);
        }
    }
    public Person? SearchPerson(string firstName, string lastName)
    {
        return _directory.Count == 0 
            ? null 
            : _directory.FirstOrDefault(person => person.FirstNameLastName == firstName + lastName);
    }

    public void Add(Person person)
    {
        _directory.AddLast(person);
    }

    public void PrintDirectory()
    {
        if (_directory.Count == 0)
        {
            Console.WriteLine("Directory is empty.");
        }

        foreach (var person in _directory)
        {
            Console.WriteLine(person);
        }
    }
}