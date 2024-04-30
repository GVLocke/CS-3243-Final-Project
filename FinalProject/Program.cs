namespace FinalProject;

internal static class Program
{
    private static void Main()
    {
        var tiny = new StreamReader("../../../tiny.csv");
        var phonebook = new RedBlackTreePhoneBook(tiny);
        var person = phonebook.SearchPerson("Una", "Nichols");
        if (person == null)
        {
            Console.WriteLine("No person found");
        } else
        {
            Console.WriteLine(person);
        }
    }
}