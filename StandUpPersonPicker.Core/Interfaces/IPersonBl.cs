using StandUpPersonPicker.Domain.Models;
using StandUpPersonPicker.Domain.Models.RickAndMorty;

namespace StandUpPersonPicker.Core.Interfaces;

public interface IPersonBl
{
    PersonResponse GetPersonByIndex(int index);
    Task<Dictionary<Character, string>> CreateCharacterPersonPairs(List<string> personNames);
}