using StandUpPersonPicker.Core.Interfaces;
using StandUpPersonPicker.Domain.Models;
using StandUpPersonPicker.Domain.Models.RickAndMorty;
using StandUpPersonPicker.Infrastructure.Dals.Interfaces;

namespace StandUpPersonPicker.Core.Implementations
{
    public class PersonBl : IPersonBl
    {
        private readonly ICharacterBl _characterBl;
        private readonly IStatisticDal _statisticDal;
		private readonly Random _random = new();

        private Dictionary<Character, string> CharacterPersonPairs { get; set; } = new();

        public PersonBl(ICharacterBl characterBl, IStatisticDal statisticDal)
        {
            _characterBl = characterBl;
            _statisticDal = statisticDal;
		}
        
        public async Task<Dictionary<Character, string>> CreateCharacterPersonPairs(List<string> personNames)
        {
            var characters = await _characterBl.GetCharacters();
            var characterPersonPairs = new Dictionary<Character, string>();
            var shuffledIndices = Enumerable.Range(0, characters.Count).OrderBy(i => _random.Next()).ToList();
            var shuffledPersonsIndices = Enumerable.Range(0, personNames.Count).OrderBy(i => _random.Next()).ToList();

            var shuffledPerson = shuffledPersonsIndices.Select(index => personNames[index]).ToList();

            foreach (var personName in shuffledPerson)
            {
                characterPersonPairs.Add(characters[shuffledIndices[0]], personName);
                shuffledIndices.RemoveAt(0);
            }

            CharacterPersonPairs = characterPersonPairs;

            var statistic = new StatisticDao
            {
                TotalNumberOfPeople = personNames.Count,
                SessionDateTime = DateTimeOffset.Now
            };
            await _statisticDal.Create(statistic);
            
			return characterPersonPairs;
        }

        public PersonResponse GetPersonByIndex(int index)
        {
            var personResponse = new PersonResponse();

            if (index < 0 || index > CharacterPersonPairs.Count)
            {
                personResponse.Errors.Add("Index not valid!");
            }
            else
            {
                personResponse.Character = CharacterPersonPairs.ElementAt(index).Key;
                personResponse.Name = CharacterPersonPairs.ElementAt(index).Value;
            }

            return personResponse;
        }
    }
}
