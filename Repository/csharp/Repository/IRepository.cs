namespace Repository;

public interface IRepository
{
    Task<IEnumerable<Exercise>> ListExercises();
    Task Add(Exercise exercise);
    Task<IEnumerable<Exercise>> FindExercisesStartedInTimePeriod(DateTime start, TimeSpan duration);
}
