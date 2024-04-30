namespace FinalProject;

internal static class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("\n" +
                              "Usage:   dotnet PATH DATA-STRUCTURE\n\n" +
                              "A searchable phone book application.\n\n" +
                              "Data Structures:\n" +
                              "\tlist\n" +
                              "\thashtable\n" +
                              "\tlinkedlist\n" +
                              "\tredblacktree\n" +
                              "\tskiplist\n");
            return;
        }
        var pathname = args[0];
        var dataStructure = args[1];
        var reader = new StreamReader(pathname);
        IPhoneBook phonebook;
        try
        {
            phonebook = CreatePhoneBook(dataStructure, reader);
            Console.WriteLine($"{dataStructure} phonebook created!");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return;
        }
        // Now you can use the phonebook object
        Console.WriteLine("Enter a first name: ");
        var firstName = Console.ReadLine();
        Console.WriteLine("Enter last name: ");
        var lastName = Console.ReadLine();
        if (firstName == null || lastName == null)
        {
            Console.WriteLine("Failed to read in names.");
        }
        var person = phonebook.SearchPerson(firstName, lastName);
        if (person == null)
        {
            Console.WriteLine("No person found");
        }
        else
        {
            Console.WriteLine(person);
        }
    }

    private static IPhoneBook CreatePhoneBook(string dataStructure, StreamReader reader)
    {
        return dataStructure switch
        {
            "list" => new ArrayPhoneBook(reader),
            "hashtable" => new HashTablePhoneBook(reader),
            "linkedlist" => new LinkedListPhoneBook(reader),
            "redblacktree" => new RedBlackTreePhoneBook(reader),
            "skiplist" => new SkipList(reader),
            _ => throw new ArgumentException(
                "Invalid data structure specified. Please choose from 'list', 'hashtable', 'linkedlist', or 'redblacktree'.")
        };
    }
}