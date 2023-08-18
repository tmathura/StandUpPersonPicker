using StandUpDeveloperPicker.Domain.Models.RickAndMorty;

namespace StandUpDeveloperPicker.Core.Interfaces;

public interface ICharacterBl
{
    Task<List<Character>> GetCharacters();

    Task<Dictionary<Character, string>> CreateCharacterDeveloperPairs(List<string> developerNames);
}