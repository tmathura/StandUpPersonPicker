using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using StandUpPersonPicker.Core.Implementations;
using StandUpPersonPicker.Core.Interfaces;
using StandUpPersonPicker.Domain.Models;

var index = 0;
var settings = new Settings();
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
configuration.Bind(settings);

var serviceProvider = new ServiceCollection()
    .AddSingleton<IConfigurationRoot, ConfigurationRoot>(_ => (ConfigurationRoot)configuration)
    .AddSingleton<IRestClient, RestClient>(_ => new RestClient(settings.ApiBaseUrl))
    .AddSingleton<ICharacterBl, CharacterBl>()
    .AddSingleton<IPersonBl, PersonBl>()
    .BuildServiceProvider();

var personBl = serviceProvider.GetService<IPersonBl>();

if (personBl == null)
{
    throw new ArgumentNullException(nameof(personBl));
}

var personNames = settings.PersonNames;

await personBl.CreateCharacterPersonPairs(personNames);

Console.WriteLine("Welcome to daily stand ups.");

while (index < personNames.Count)
{
    var personResponse = personBl.GetPersonByIndex(index);
    Console.ForegroundColor = GetRandomConsoleColor();
    Console.WriteLine($"The current person to speak is '{personResponse.Name}' and their character is '{personResponse.Character.Name}'.");
    Console.WriteLine("Press enter to show the next person.");
    Console.ReadLine();

    index++;
}

return;

static ConsoleColor GetRandomConsoleColor()
{
    var random = new Random();

    var consoleColors = Enum.GetValues(typeof(ConsoleColor))
        .Cast<ConsoleColor>()
        .Where(color => color != ConsoleColor.Black)
        .ToArray();

    return (ConsoleColor)(consoleColors.GetValue(random.Next(consoleColors.Length)) ?? ConsoleColor.Green);
}