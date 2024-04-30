using System.ComponentModel;

namespace FinalProject;

public class RandomData(
    IReadOnlyList<string> firstNames,
    IReadOnlyList<string> lastNames,
    IReadOnlyList<string> domainNames,
    IReadOnlyList<string> addressNames
    )
{
    public string GetRandomPhoneBookEntry()
    {
        var random = new Random();
        var middleInitial = random.Next(65, 91);
        var firstName = GetRandomValue(DataRequest.FirstName);
        var lastName = GetRandomValue(DataRequest.LastName);
        var email = $"{firstName}{Convert.ToChar(middleInitial)}{lastName}@{GetRandomValue(DataRequest.Domain)}";
        return $"{firstName}," +
               $"{lastName}," +
               $"{GetRandomValue(DataRequest.Address)}," +
               $"{email}," +
               $"{GenerateRandomPhoneNumber()}";
    }
    
    private static string GenerateRandomPhoneNumber()
    {
        var random = new Random();
        var phoneNumber = string.Empty;

        // Generate area code
        for (var i = 0; i < 3; i++)
        {
            phoneNumber += random.Next(0, 10).ToString();
        }

        phoneNumber += "-";

        // Generate first 3 digits
        for (var i = 0; i < 3; i++)
        {
            phoneNumber += random.Next(0, 10).ToString();
        }

        phoneNumber += "-";

        // Generate last 4 digits
        for (var i = 0; i < 4; i++)
        {
            phoneNumber += random.Next(0, 10).ToString();
        }

        return phoneNumber;
    }
    
    private string GetRandomValue(DataRequest request)
    {
        var random = new Random();
        return request switch
        {
            DataRequest.Address => addressNames[random.Next(addressNames.Count)],
            DataRequest.FirstName => firstNames[random.Next(firstNames.Count)],
            DataRequest.LastName => lastNames[random.Next(lastNames.Count)],
            DataRequest.Domain => domainNames[random.Next(domainNames.Count)],
            _ => throw new InvalidEnumArgumentException()
        };
    }
}