using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using StandUpDeveloperPicker.Core.Implementations;
using StandUpDeveloperPicker.Core.Interfaces;
using StandUpDeveloperPicker.Domain.Models;

var settings = new Settings();
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
configuration.Bind(settings);

var serviceProvider = new ServiceCollection()
    .AddSingleton<IRestClient, RestClient>(_ => new RestClient(settings.ApiBaseUrl))
    .AddSingleton<ICharacterBl, CharacterBl>()
    .BuildServiceProvider();

var characterBl = serviceProvider.GetService<ICharacterBl>();

if (characterBl == null)
{
    throw new ArgumentNullException(nameof(characterBl));
}

var developerNames = settings.DeveloperNames;

var characterDeveloperPairs = await characterBl.CreateCharacterDeveloperPairs(developerNames);

foreach (var characterDeveloperPair in characterDeveloperPairs)
{
    Console.WriteLine($"The developer is '{characterDeveloperPair.Value}' and their character is '{characterDeveloperPair.Key.Name}'.");
}