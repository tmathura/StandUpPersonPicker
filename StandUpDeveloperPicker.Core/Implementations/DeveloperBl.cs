using Microsoft.Extensions.Configuration;
using StandUpDeveloperPicker.Core.Interfaces;
using StandUpDeveloperPicker.Domain.Models;
using StandUpDeveloperPicker.Domain.Models.RickAndMorty;

namespace StandUpDeveloperPicker.Core.Implementations
{
    public class DeveloperBl : IDeveloperBl
    {
        private readonly ICharacterBl _characterBl;
        private readonly Random _random = new();
        private List<string> DeveloperNames { get; }

        public DeveloperBl(ICharacterBl characterBl, IConfigurationRoot configuration)
        {
            var settings = new Settings();
            configuration.Bind(settings);

            _characterBl = characterBl;

            DeveloperNames = settings.DeveloperNames;
        }
        
        public async Task<Dictionary<Character, string>> CreateCharacterDeveloperPairs()
        {
            var characters = await _characterBl.GetCharacters();
            var characterDeveloperPairs = new Dictionary<Character, string>();
            var shuffledIndices = Enumerable.Range(0, characters.Count).OrderBy(i => _random.Next()).ToList();
            var shuffledDevelopersIndices = Enumerable.Range(0, DeveloperNames.Count).OrderBy(i => _random.Next()).ToList();

            var shuffledDeveloper = new List<string>();

            for (var index = 0; index < shuffledDevelopersIndices.Count; index++)
            {
                shuffledDeveloper.Add(DeveloperNames[shuffledDevelopersIndices[index]]);
            }

            foreach (var developerName in shuffledDeveloper)
            {
                characterDeveloperPairs.Add(characters[shuffledIndices[0]], developerName);
                shuffledIndices.RemoveAt(0);
            }

            return characterDeveloperPairs;
        }
    }
}
