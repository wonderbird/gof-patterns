namespace Repository;

public class ExerciseController
{
    private readonly InMemoryRepository _repository = new();

    private List<Exercise> Exercises => _repository.Exercises;

    public IEnumerable<Exercise> ListExercises() => Exercises;

    public void Add(Exercise exercise) => Exercises.Add(exercise);

    public IEnumerable<Exercise> FindExercisesStartedInTimePeriod(DateTime start, TimeSpan duration)
    {
        var end = start + duration;
        return Exercises.Where(e => e.Start >= start && e.Start <= end);
    }
}
