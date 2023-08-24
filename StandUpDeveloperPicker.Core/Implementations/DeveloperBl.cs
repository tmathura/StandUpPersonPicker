using StandUpDeveloperPicker.Core.Interfaces;
using StandUpDeveloperPicker.Domain.Models;
using StandUpDeveloperPicker.Domain.Models.RickAndMorty;

namespace StandUpDeveloperPicker.Core.Implementations
{
    public class DeveloperBl : IDeveloperBl
    {
        private readonly ICharacterBl _characterBl;
        private readonly Random _random = new();

        private Dictionary<Character, string> CharacterDeveloperPairs { get; set; } = new();

        public DeveloperBl(ICharacterBl characterBl)
        {
            _characterBl = characterBl;
        }
        
        public async Task CreateCharacterDeveloperPairs(List<string> developerNames)
        {
            var characters = await _characterBl.GetCharacters();
            var characterDeveloperPairs = new Dictionary<Character, string>();
            var shuffledIndices = Enumerable.Range(0, characters.Count).OrderBy(i => _random.Next()).ToList();
            var shuffledDevelopersIndices = Enumerable.Range(0, developerNames.Count).OrderBy(i => _random.Next()).ToList();

            var shuffledDeveloper = shuffledDevelopersIndices.Select(index => developerNames[index]).ToList();

            foreach (var developerName in shuffledDeveloper)
            {
                characterDeveloperPairs.Add(characters[shuffledIndices[0]], developerName);
                shuffledIndices.RemoveAt(0);
            }

            CharacterDeveloperPairs = characterDeveloperPairs;
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
