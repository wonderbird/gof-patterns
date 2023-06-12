namespace Repository;

public class InMemoryRepository : IRepository
{
    private readonly List<Exercise> _exercises = new();

    public async Task<IEnumerable<Exercise>> ListExercises() => await Task.Run(() => _exercises);

    public async Task Add(Exercise exercise) => await Task.Run(() => _exercises.Add(exercise));

    public async Task<IEnumerable<Exercise>> FindExercisesStartedInTimePeriod(
        DateTime start,
        TimeSpan duration
    )
    {
        var end = start + duration;
        return await Task.Run(() => _exercises.Where(e => e.Start >= start && e.Start <= end));
    }
}
