using RestSharp;
using StandUpDeveloperPicker.Core.Interfaces;
using StandUpDeveloperPicker.Domain.Models;
using StandUpDeveloperPicker.Domain.Models.RickAndMorty;

namespace StandUpDeveloperPicker.Core.Implementations
{
    public class CharacterBl : ICharacterBl
    {
        private readonly IRestClient _restClient;
        private readonly Random _random = new();

        public CharacterBl(IRestClient restClient)
        {
            this._restClient = restClient;
        }

        public async Task<List<Character>> GetCharacters()
        {
            var restRequest = new RestRequest("character");
            var response = await _restClient.ExecuteGetAsync<CharacterResponse>(restRequest);
            var responseData = response.Data?.Results;

            return responseData ?? new List<Character>();
        }

        public async Task<Dictionary<Character, string>> CreateCharacterDeveloperPairs(List<string> developerNames)
        {
            var characters = await GetCharacters();
            var characterDeveloperPairs = new Dictionary<Character, string>();
            var shuffledIndices = Enumerable.Range(0, characters.Count).OrderBy(i => _random.Next()).ToList();
            
            foreach (var developerName in developerNames)
            {
                characterDeveloperPairs.Add(characters[shuffledIndices[0]], developerName);
                shuffledIndices.RemoveAt(0);
            }

            return characterDeveloperPairs;
        }
    }
}
