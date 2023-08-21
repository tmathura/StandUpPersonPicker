using StandUpDeveloperPicker.Domain.Models;

namespace StandUpDeveloperPicker.Core.Interfaces;

public interface IDeveloperBl
{
    int DeveloperCount { get; }
    DeveloperResponse GetDeveloperByIndex(int index);
}