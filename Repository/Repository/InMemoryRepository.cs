namespace Repository;

public class InMemoryRepository
{
    public List<Exercise> Exercises { get; set; } = new();

    public IEnumerable<Exercise> ListExercises() => Exercises;

    public void Add(Exercise exercise) => Exercises.Add(exercise);

    public IEnumerable<Exercise> FindExercisesStartedInTimePeriod(DateTime start, TimeSpan duration)
    {
        var end = start + duration;
        return Exercises.Where(e => e.Start >= start && e.Start <= end);
    }
}
