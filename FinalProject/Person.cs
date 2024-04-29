namespace FinalProject
{
    public class Person(
        string firstNameLastName,
        string address,
        string city,
        string state,
        int zipCode,
        string email,
        string telephoneNumber)
        : IEquatable<Person>, IComparable<Person>
    {
        private string FirstNameLastName { get; } = firstNameLastName;
        private string Address { get; } = address;
        private string City { get; } = city;
        private string State { get; } = state;
        private int ZipCode { get; } = zipCode;
        private string Email { get; } = email;
        private string TelephoneNumber { get; } = telephoneNumber;

        public bool Equals(Person? other)
        {
            if (other == null)
                return false;

            return FirstNameLastName == other.FirstNameLastName &&
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
            return this.FirstNameLastName.GetHashCode() ^
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
    }
}