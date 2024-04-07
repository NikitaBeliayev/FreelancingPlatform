namespace Application.Objectives.Types.RequestDto;

public class UpdateObjectiveTypeRequestDto
{
    public int Duration { get; set; }
    public string Title { get; set; }
    public Guid Id { get; set; }
}