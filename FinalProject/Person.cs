namespace FinalProject
{
    public class Person(
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
        : IEquatable<Person>, IComparable<Person>
    {
        public int Id { get; } = id;
        public string FirstNameLastName { get; } = firstNameLastName;
        public string FirstName { get; } = firstName;
        public string LastName { get; } = lastName;
        public string Address { get; } = address;
        public string City { get; } = city;
        public string State { get; } = state;
        public int ZipCode { get; } = zipCode;
        public string Email { get; } = email;
        public string TelephoneNumber { get; } = telephoneNumber;

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
            return this.Id ^
                   this.FirstNameLastName.GetHashCode() ^
                   this.Address.GetHashCode() ^
                   this.City.GetHashCode() ^
                   this.State.GetHashCode() ^
                   this.ZipCode.GetHashCode() ^
                   this.Email.GetHashCode() ^
                   this.TelephoneNumber.GetHashCode();
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
}