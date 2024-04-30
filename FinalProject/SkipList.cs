using Maybe.SkipList;

namespace FinalProject;

public class SkipList : IPhoneBook
{
    private readonly SkipList<Person> _directory = [];

    public SkipList(StreamReader file)
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
                     id: int.Parse(parts[0]),
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
            _directory.Add(person);
        }
    }
    public Person? SearchPerson(string firstName, string lastName)
    {
        return !_directory.Any() 
            ? null 
            : _directory.FirstOrDefault(person => person.FirstNameLastName == firstName + lastName);
    }

    public int GetSize()
    {
        return _directory.Count();
    }

    public void Add(Person person)
    {
        _directory.Add(person);
    }
}