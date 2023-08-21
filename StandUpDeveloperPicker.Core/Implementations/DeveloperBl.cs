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
        public int DeveloperCount { get; }
        private Dictionary<Character, string> CharacterDeveloperPairs { get; }

        public DeveloperBl(ICharacterBl characterBl, IConfigurationRoot configuration)
        {
            var settings = new Settings();
            configuration.Bind(settings);

            _characterBl = characterBl;

            DeveloperNames = settings.DeveloperNames;
            DeveloperCount = DeveloperNames.Count;

            CharacterDeveloperPairs = CreateCharacterDeveloperPairs();
        }
        
        private Dictionary<Character, string> CreateCharacterDeveloperPairs()
        {
            var characters = _characterBl.GetCharacters().Result;
            var characterDeveloperPairs = new Dictionary<Character, string>();
            var shuffledIndices = Enumerable.Range(0, characters.Count).OrderBy(i => _random.Next()).ToList();
            var shuffledDevelopersIndices = Enumerable.Range(0, DeveloperNames.Count).OrderBy(i => _random.Next()).ToList();

            var shuffledDeveloper = shuffledDevelopersIndices.Select(index => DeveloperNames[index]).ToList();

            foreach (var developerName in shuffledDeveloper)
            {
                characterDeveloperPairs.Add(characters[shuffledIndices[0]], developerName);
                shuffledIndices.RemoveAt(0);
            }

            return characterDeveloperPairs;
        }

        public DeveloperResponse GetDeveloperByIndex(int index)
        {
            var developerResponse = new DeveloperResponse();

            if (index < 0 || index > CharacterDeveloperPairs.Count)
            {
                developerResponse.Errors.Add("Index not valid!");
            }
            else
            {
                developerResponse.Character = CharacterDeveloperPairs.ElementAt(index).Key;
                developerResponse.Name = CharacterDeveloperPairs.ElementAt(index).Value;
            }

            return developerResponse;
        }
    }
}
