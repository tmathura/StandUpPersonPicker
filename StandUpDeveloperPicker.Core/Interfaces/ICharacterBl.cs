﻿using StandUpDeveloperPicker.Domain.Models.RickAndMorty;

namespace StandUpDeveloperPicker.Core.Interfaces;

public interface ICharacterBl
{
    Task<List<Character>> GetCharacters();
}