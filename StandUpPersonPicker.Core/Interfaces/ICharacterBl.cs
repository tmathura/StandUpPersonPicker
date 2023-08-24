using StandUpPersonPicker.Domain.Models.RickAndMorty;

namespace StandUpPersonPicker.Core.Interfaces;

public interface ICharacterBl
{
    Task<List<Character>> GetCharacters();
}