namespace Repository;

public interface IRepository
{
    IEnumerable<Exercise> ListExercises();
    void Add(Exercise exercise);
    IEnumerable<Exercise> FindExercisesStartedInTimePeriod(DateTime start, TimeSpan duration);
}
