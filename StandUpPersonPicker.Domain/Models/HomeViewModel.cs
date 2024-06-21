namespace StandUpPersonPicker.Domain.Models;

public class HomeViewModel
{
    public List<Person> People { get; set; }
    public string AllPeopleNames { get; set; }
    public List<string> SelectedPeople { get; set; }
}