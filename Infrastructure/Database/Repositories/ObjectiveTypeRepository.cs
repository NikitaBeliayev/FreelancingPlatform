using Domain.Repositories;
using Domain.Types;

namespace Infrastructure.Database.Repositories;

public class ObjectiveTypeRepository : Repository<ObjectiveType>, IObjectiveTypeRepository
{
    public ObjectiveTypeRepository(AppDbContext dbContext) : base(dbContext)
    {
        
    }
}