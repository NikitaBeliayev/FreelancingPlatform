using Domain.Objectives;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Infrastructure.Database.Repositories;

public class ObjectiveRepository : Repository<Objective>, IObjectiveRepository
{
    public ObjectiveRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}