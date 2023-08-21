using StandUpDeveloperPicker.Domain.Models.RickAndMorty;

namespace StandUpDeveloperPicker.Domain.Models
{
    public class DeveloperResponse
    {
        public string Name { get; set; }
        public Character Character { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}