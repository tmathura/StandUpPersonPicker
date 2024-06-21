namespace StandUpPersonPicker.Domain.Models;

public class HomeViewModel
{
    public List<Person> People { get; set; } = new List<Person>();
    public string? AllPeopleNames { get; set; } = null;
    public List<string> SelectedPeople { get; set; } = new List<string>();
}