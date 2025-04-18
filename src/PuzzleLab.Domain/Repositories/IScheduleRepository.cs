using PuzzleLab.Domain.Entities;

namespace PuzzleLab.Domain.Repositories;

public interface IScheduleRepository
{
    Task AddScheduleAsync(Schedule schedule, CancellationToken cancellationToken = default);
    Task<List<Schedule>> GetAllSchedulesAsync(CancellationToken cancellationToken = default);
    Task<Schedule?> GetScheduleByIdAsync(Guid scheduleId, CancellationToken cancellationToken = default);
}