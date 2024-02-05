using Application.Objectives.RequestDto;
using Domain.Objectives;
using Domain.Users.UserDetails;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class ObjectiveRepository : IObjectiveRepository
{
    private readonly AppDbContext _context;
    
    public ObjectiveRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Objective?> GetObjectiveByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Objectives.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IEnumerable<Objective>?> GetObjectiveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        User? possibleUser = await _context.Users.Include(e => e.Objectives).FirstOrDefaultAsync(e => e.Id == userId, cancellationToken);
        return possibleUser?.Objectives;
    }

    public async Task<Objective?> DeleteObjectiveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Objective? possibleObjective = await GetObjectiveByIdAsync(id, cancellationToken);
        return possibleObjective is null ? null : _context.Objectives.Remove(possibleObjective).Entity;
    }
    
    public async Task<Objective?> CreateObjectiveAsync(Objective objective, CancellationToken cancellationToken = default)
    {
        await _context.Objectives.AddAsync(objective, cancellationToken);
        return objective;
    }
    
    public Objective UpdateObjectiveAsync(Objective objective, CancellationToken cancellationToken = default)
    {
        _context.Objectives.Update(objective);
        return objective;
    }
}