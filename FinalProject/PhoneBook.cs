namespace FinalProject;

public interface IPhoneBook
{ 
    Person? SearchPerson(string firstName, string lastName);
    int GetSize();
    void Add(Person person);
}