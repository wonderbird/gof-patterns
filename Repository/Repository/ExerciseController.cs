namespace Repository;

public class ExerciseController
{
    private readonly IRepository _repository;

    public ExerciseController(IRepository repository) => _repository = repository;

    public IEnumerable<Exercise> ListExercises() => _repository.ListExercises();

    public void Add(Exercise exercise) => _repository.Add(exercise);

    public IEnumerable<Exercise> FindExercisesStartedInTimePeriod(
        DateTime start,
        TimeSpan duration
    ) => _repository.FindExercisesStartedInTimePeriod(start, duration);
}
