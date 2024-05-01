namespace FinalProject;

public class ArrayPhoneBook : IPhoneBook
{
    private readonly List<Person> _directory = [];
    public ArrayPhoneBook(StreamReader file)
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
    _directory.Sort();
    var target = firstName + lastName;
    var left = 0;
    var right = _directory.Count - 1;

    while (left <= right)
    {
        var mid = left + (right - left) / 2;
        var midName = _directory[mid].FirstNameLastName;

        if (midName == target)
        {
            return _directory[mid];
        }
        if (string.Compare(midName, target, StringComparison.Ordinal) < 0)
        {
            left = mid + 1;
        }
        else
        {
            right = mid - 1;
        }
    }
    return null;
}

    public int GetSize()
    {
        return _directory.Count;
    }

    public void Add(Person person)
    {
        _directory.Add(person);
    }
}