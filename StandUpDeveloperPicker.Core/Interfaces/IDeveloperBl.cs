using StandUpDeveloperPicker.Domain.Models.RickAndMorty;

namespace StandUpDeveloperPicker.Core.Interfaces;

public interface IDeveloperBl
{
    Task<Dictionary<Character, string>> CreateCharacterDeveloperPairs();
}