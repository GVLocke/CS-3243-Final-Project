using System.Diagnostics;

namespace FinalProject;

internal static class Program
{
    private const string AddressesPath = "generation_data/addresses.txt";
    private const string FirstNamesPath = "generation_data/first-names.txt";
    private const string LastNamesPath = "generation_data/last-names.txt";
    private const string DomainsPath = "generation_data/opendns-top-domains.txt";
    private static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("\n" +
                              "Usage:   dotnet PATH DATA-STRUCTURE COMMAND [OPTIONS]\n\n" +
                              "A searchable phone book application.\n\n" +
                              "Data Structures:\n" +
                              "\tlist\n" +
                              "\thashtable\n" +
                              "\tlinkedlist\n" +
                              "\tredblacktree\n" +
                              "\tskiplist\n\n" +
                              "Commands:\n" +
                              "\tsearch: searches for a single entry in the phonebook\n" +
                              "\tsearch [ENTRIES]: searches for ENTRIES number of entries at the end of the starter " +
                              "data to test for speed.\n" +
                              "\tinsert [ENTRIES]: inserts ENTRIES number of random values into the phonebook.");
            return;
        }
        var pathname = args[0];
        var dataStructure = args[1];
        var reader = new StreamReader(pathname);
        IPhoneBook phonebook;
        try
        {
            var before = GC.GetTotalMemory(true);
            phonebook = CreatePhoneBook(dataStructure, reader);
            var after = GC.GetTotalMemory(true);
            var memoryUsed = after - before;
            Console.WriteLine($"Memory used by CreatePhoneBook: {memoryUsed} bytes");
            Console.WriteLine($"{dataStructure} phonebook created!");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return;
        }

        switch (args[2])
        {
            case "search" when args.Length == 3:
            {
                Console.WriteLine("Enter a first name: ");
                var firstName = Console.ReadLine();
                Console.WriteLine("Enter last name: ");
                var lastName = Console.ReadLine();
                if (firstName == null || lastName == null)
                {
                    Console.WriteLine("Failed to read in names.");
                    return;
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
                break;
            }
            case "search" when args.Length == 4:
                if (int.Parse(args[3]) >= 1)
                {
                    var before = GC.GetTotalMemory(true);
                    var timespan = PerformSearches(int.Parse(args[3]), phonebook, new StreamReader(args[0]));
                    var after = GC.GetTotalMemory(true);
                    Console.WriteLine($"\nTime to search for {int.Parse(args[3])} entries: {timespan.TotalSeconds} seconds\n" +
                                      $"Memory Used by PerformSearches: {after - before} bytes.");
                }
                else
                {
                    Console.WriteLine("Invalid arguments. Please try again.");
                }
                break;
                
            default:
            {
                if (args.Length == 4 && int.Parse(args[3]) >= 1)
                {
                    var before = GC.GetTotalMemory(true);
                    var timespan = InsertNewEntries(int.Parse(args[3]), phonebook);
                    var after = GC.GetTotalMemory(true);
                    Console.WriteLine($"\nTime to insert {int.Parse(args[3])} entries: {timespan.TotalSeconds} seconds\n" +
                                      $"Memory Used by InsertNewEntries: {after - before} bytes.");
                }
                else
                {
                    Console.WriteLine("Invalid arguments. Please try again.");
                }

                break;
            }
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

    private static TimeSpan InsertNewEntries(int count, IPhoneBook phoneBook)
    {
        var data = GenerateRandomDataRecord();
        var stopwatch = new Stopwatch();
        var progressBar = new ProgressBar(count);
        for (var i = 0; i <= count; i++)
        {
            var insertion = data.GetRandomPhoneBookEntry();
            var person = new Person($"{phoneBook.GetSize() + 1},{insertion}");
            stopwatch.Start();
            phoneBook.Add(person);
            stopwatch.Stop();
            progressBar.Increment();
        }
        return stopwatch.Elapsed;
    }
    
    private static RandomData GenerateRandomDataRecord()
    {
        var addresses = File.ReadAllLines(AddressesPath);
        var firstNames = File.ReadAllLines(FirstNamesPath);
        var lastNames = File.ReadAllLines(LastNamesPath);
        var domains = File.ReadAllLines(DomainsPath);
        return new RandomData(firstNames, lastNames, domains, addresses);
    }

    private static TimeSpan PerformSearches(int count, IPhoneBook phoneBook, StreamReader file)
    {
        var lastCountLines = new Queue<string?>(count);
        using (file)
        {
            while (!file.EndOfStream)
            {
                lastCountLines.Enqueue(file.ReadLine());
                if (lastCountLines.Count > count)
                {
                    lastCountLines.Dequeue();
                }
            }
        }

        var stopwatch = new Stopwatch();
        var progressBar = new ProgressBar(count);
        foreach (
            var parts in lastCountLines.Select(text => text?.Split(',') 
                                                       ?? []).TakeWhile(parts => parts.Length != 0))
        {
            stopwatch.Start();
            phoneBook.SearchPerson(parts[1], parts[2]);
            stopwatch.Stop();
            progressBar.Increment();
        }
        return stopwatch.Elapsed;
    }
}