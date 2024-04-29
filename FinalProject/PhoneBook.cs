namespace FinalProject;

public interface IPhoneBook
{ 
    Person? SearchPerson(string firstName, string lastName);
    void Add(Person person);
    void PrintDirectory();
}