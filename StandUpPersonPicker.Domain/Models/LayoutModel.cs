namespace StandUpPersonPicker.Domain.Models;

public class LayoutModel
{
    public LayoutModel(int sessionsSoFar)
    {
	    SessionsSoFar = sessionsSoFar;
    }

    public int SessionsSoFar { get; }
}

public class LayoutModel<T> : LayoutModel
{
    public LayoutModel(T pageModel, int sessionsSoFar) : base(sessionsSoFar)
    {
        PageModel = pageModel;
    }

    public T PageModel { get; }
}