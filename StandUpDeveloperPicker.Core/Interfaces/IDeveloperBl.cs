using StandUpDeveloperPicker.Domain.Models;

namespace StandUpDeveloperPicker.Core.Interfaces;

public interface IDeveloperBl
{
    DeveloperResponse GetDeveloperByIndex(int index);
    Task CreateCharacterDeveloperPairs(List<string> developerNames);
}