using System.ComponentModel;
using System.Text;

namespace FinalProject;

public readonly struct RandomData(
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
    var phoneNumber = new StringBuilder();

    // Generate area code
    for (var i = 0; i < 3; i++)
    {
        phoneNumber.Append(random.Next(0, 10).ToString());
    }

    phoneNumber.Append('-');

    // Generate first 3 digits
    for (var i = 0; i < 3; i++)
    {
        phoneNumber.Append(random.Next(0, 10).ToString());
    }

    phoneNumber.Append('-');

    // Generate last 4 digits
    for (var i = 0; i < 4; i++)
    {
        phoneNumber.Append(random.Next(0, 10).ToString());
    }

    return phoneNumber.ToString();
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