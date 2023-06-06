namespace Repository;

public class ExerciseController
{
    private readonly InMemoryRepository _repository = new();

    public IEnumerable<Exercise> ListExercises() => _repository.ListExercises();

    public void Add(Exercise exercise) => _repository.Add(exercise);

    public IEnumerable<Exercise> FindExercisesStartedInTimePeriod(
        DateTime start,
        TimeSpan duration
    ) => _repository.FindExercisesStartedInTimePeriod(start, duration);
}
