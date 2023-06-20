namespace Repository;

public class EntityFrameworkRepository : IRepository
{
    public EntityFrameworkRepository(string fixtureConnectionString) => throw new NotImplementedException();

    public async Task<IEnumerable<Exercise>> ListExercises() => throw new NotImplementedException();

    public async Task Add(Exercise exercise) => throw new NotImplementedException();

    public async Task<IEnumerable<Exercise>> FindExercisesStartedInTimePeriod(DateTime start, TimeSpan duration) => throw new NotImplementedException();
}