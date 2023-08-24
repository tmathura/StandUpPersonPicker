using StandUpPersonPicker.Domain.Models;

namespace StandUpPersonPicker.Core.Interfaces;

public interface IPersonBl
{
    PersonResponse GetPersonByIndex(int index);
    Task CreateCharacterPersonPairs(List<string> personNames);
}