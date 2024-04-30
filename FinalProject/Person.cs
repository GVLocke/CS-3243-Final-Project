namespace FinalProject;

public class Person : IEquatable<Person>, IComparable<Person>
{
    private int Id { get; }
    public string FirstNameLastName { get; }
    private string FirstName { get; }
    private string LastName { get; }
    private string Address { get; }
    private string City { get; }
    private string State { get; }
    private int ZipCode { get; }
    private string Email { get; }
    private string TelephoneNumber { get; }

    public Person(
        int id,
        string firstNameLastName,
        string firstName,
        string lastName,
        string address,
        string city,
        string state,
        int zipCode,
        string email,
        string telephoneNumber)
    {
        Id = id;
        FirstNameLastName = firstNameLastName;
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        City = city;
        State = state;
        ZipCode = zipCode;
        Email = email;
        TelephoneNumber = telephoneNumber;
    }
    
    public Person(string data)
    {
        var parts = data.Split(',');
        Id = int.Parse(parts[0]);
        FirstNameLastName = parts[1] + " " + parts[2];
        FirstName = parts[1];
        LastName = parts[2];
        Address = parts[3];
        City = parts[4];
        State = parts[5];
        ZipCode = int.Parse(parts[6]);
        Email = parts[7];
        TelephoneNumber = parts[8];
    }

    public bool Equals(Person? other)
    {
        if (other == null)
            return false;

        return Id == other.Id &&
               FirstNameLastName == other.FirstNameLastName &&
               Address == other.Address &&
               City == other.City &&
               State == other.State &&
               ZipCode == other.ZipCode &&
               Email == other.Email &&
               TelephoneNumber == other.TelephoneNumber;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        return obj is Person personObj && Equals(personObj);
    }

    public override int GetHashCode()
    {
        return Id ^
               FirstNameLastName.GetHashCode() ^
               Address.GetHashCode() ^
               City.GetHashCode() ^
               State.GetHashCode() ^
               ZipCode.GetHashCode() ^
               Email.GetHashCode() ^
               TelephoneNumber.GetHashCode();
    }

    public int CompareTo(Person? obj)
    {
        return obj == null ? 1 : string.Compare(FirstNameLastName, obj.FirstNameLastName, StringComparison.Ordinal);
    }

    public override string ToString()
    {
        return $"Id: {Id}\n" +
               $"Name: {FirstName} {LastName}\n" +
               $"Address: {Address}\n" +
               $"City: {City}\n" +
               $"State: {State}\n" +
               $"ZipCode: {ZipCode}\n" +
               $"Email: {Email}\n" +
               $"Telephone: {TelephoneNumber}";
    }
}
