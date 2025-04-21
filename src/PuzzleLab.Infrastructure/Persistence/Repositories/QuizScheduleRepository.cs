using Microsoft.EntityFrameworkCore;
using PuzzleLab.Domain.Entities;
using PuzzleLab.Domain.Repositories;

namespace PuzzleLab.Infrastructure.Persistence.Repositories;

public class ScheduleRepository(DatabaseContext databaseContext) : IScheduleRepository
{
    public async Task<List<Schedule>> GetAllSchedulesAsync(CancellationToken cancellationToken = default)
    {
        return await databaseContext.Schedules.ToListAsync(cancellationToken);
    }

    public async Task<Schedule?> GetScheduleByIdAsync(Guid scheduleId, CancellationToken cancellationToken = default)
    {
        return await databaseContext.Schedules.FirstOrDefaultAsync(x => x.Id == scheduleId, cancellationToken);
    }

    public async Task InsertScheduleAsync(Schedule schedule, CancellationToken cancellationToken = default)
    {
        await databaseContext.Schedules.AddAsync(schedule, cancellationToken);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateScheduleAsync(Schedule schedule, CancellationToken cancellationToken = default)
    {
        databaseContext.Schedules.Update(schedule);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteScheduleAsync(Schedule schedule, CancellationToken cancellationToken = default)
    {
        databaseContext.Schedules.Remove(schedule);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}