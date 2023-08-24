using StandUpPersonPicker.Domain.Models.RickAndMorty;

namespace StandUpPersonPicker.Domain.Models
{
    public class PersonResponse
    {
        public string Name { get; set; }
        public Character Character { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}