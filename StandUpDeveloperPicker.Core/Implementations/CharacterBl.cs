﻿using RestSharp;
using StandUpDeveloperPicker.Core.Interfaces;
using StandUpDeveloperPicker.Domain.Models;
using StandUpDeveloperPicker.Domain.Models.RickAndMorty;

namespace StandUpDeveloperPicker.Core.Implementations
{
    public class CharacterBl : ICharacterBl
    {
        private readonly IRestClient _restClient;
        private readonly int _maxNumberOfCharactersPages = 42;

        public CharacterBl(IRestClient restClient)
        {
            this._restClient = restClient;
        }

        public async Task<List<Character>> GetCharacters()
        {
            var characters = new List<Character>();

            for (var pageIndex = 0; pageIndex < _maxNumberOfCharactersPages; pageIndex++)
            {
                var restRequest = new RestRequest($"character?page={pageIndex}");
                var response = await _restClient.ExecuteGetAsync<CharacterResponse>(restRequest);
                if (response.Data?.Results != null)
                {
                    characters.AddRange(response.Data.Results);
                }
            }

            return characters;
        }
    }
}
