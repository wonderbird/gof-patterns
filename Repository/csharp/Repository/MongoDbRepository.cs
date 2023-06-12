using MongoDB.Driver;

namespace Repository;

public static class ExerciseEnumerableExtensions
{
    public static IEnumerable<Exercise> ToLocalTime(this IEnumerable<Exercise> exercisesUtc) =>
        exercisesUtc.Select(e => new Exercise(e.Start.ToLocalTime()));
}

public class MongoDbRepository : IRepository
{
    private readonly IMongoCollection<Exercise> _exercises;

    public MongoDbRepository(string connectionString)
    {
        var mongoClient = new MongoClient(connectionString);
        var mongoDatabase = mongoClient.GetDatabase("exercises");
        _exercises = mongoDatabase.GetCollection<Exercise>("Exercises");
    }

    public async Task<IEnumerable<Exercise>> ListExercises()
    {
        var exercisesUtc = await _exercises.Find(_ => true).ToListAsync();
        return exercisesUtc.ToLocalTime();
    }

    public async Task Add(Exercise exercise) => await _exercises.InsertOneAsync(exercise);

    public async Task<IEnumerable<Exercise>> FindExercisesStartedInTimePeriod(
        DateTime start,
        TimeSpan duration
    )
    {
        var startUtc = start.ToUniversalTime();
        var exercisesUtc = await _exercises
            .Find(exercise => exercise.Start >= startUtc && exercise.Start <= startUtc + duration)
            .ToListAsync();
        return exercisesUtc.ToLocalTime();
    }
}
