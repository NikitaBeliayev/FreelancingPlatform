using System.Linq.Expressions;
using Domain.Objectives;
using Domain.Repositories;
using Domain.Users.UserDetails;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class ObjectiveRepository : Repository<Objective>, IObjectiveRepository
{
    public ObjectiveRepository(AppDbContext dbContext) : base(dbContext)
    { 
    }
    
}