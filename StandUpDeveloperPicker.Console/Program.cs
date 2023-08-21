using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using StandUpDeveloperPicker.Core.Implementations;
using StandUpDeveloperPicker.Core.Interfaces;
using StandUpDeveloperPicker.Domain.Models;

var index = 0;
var settings = new Settings();
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
configuration.Bind(settings);

var serviceProvider = new ServiceCollection()
    .AddSingleton<IConfigurationRoot, ConfigurationRoot>(_ => (ConfigurationRoot)configuration)
    .AddSingleton<IRestClient, RestClient>(_ => new RestClient(settings.ApiBaseUrl))
    .AddSingleton<ICharacterBl, CharacterBl>()
    .AddSingleton<IDeveloperBl, DeveloperBl>()
    .BuildServiceProvider();

var developerBl = serviceProvider.GetService<IDeveloperBl>();

if (developerBl == null)
{
    throw new ArgumentNullException(nameof(developerBl));
}

Console.WriteLine("Welcome to daily stand ups.");

while (index < developerBl.DeveloperCount)
{
    var developerResponse = developerBl.GetDeveloperByIndex(index);
    Console.ForegroundColor = GetRandomConsoleColor();
    Console.WriteLine($"The current person to speak is '{developerResponse.Name}' and their character is '{developerResponse.Character.Name}'.");
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