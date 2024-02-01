using Domain.Roles.Errors;
using Domain.Roles;
using Shared;
using Domain.Objectives.ObjectiveTypes.Errors;

namespace Domain.Objectives.ObjectiveTypes;

public sealed record class ObjectiveTypeTitle
{
	private ObjectiveTypeTitle(ObjectiveTypeVariations objectiveTypeVariations)
	{
		Title = objectiveTypeVariations.ToString();
	}

	public string Title { get; }

	public static Result<ObjectiveTypeTitle> BuildObjectiveTypeTitle(int objectiveType)
	{
		if (objectiveType < 1 || objectiveType > 3)
		{
			return Result<ObjectiveTypeTitle>.Failure(null, ObjectiveTypeErrors.InvalidTitle);
		}

		return Result<ObjectiveTypeTitle>.Success(new ObjectiveTypeTitle((ObjectiveTypeVariations)objectiveType));
	}
}