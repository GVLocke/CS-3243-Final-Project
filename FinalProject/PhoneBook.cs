namespace FinalProject;

public abstract class PhoneBook
{ 
    public abstract Person? SearchPerson(string firstName, string lastName);
    public abstract void Insert(Person person);
    public abstract void PrintDirectory();
}