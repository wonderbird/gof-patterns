namespace Repository;

public class ExerciseController
{
    private readonly IRepository _repository;

    public ExerciseController(IRepository repository) => _repository = repository;

    public async Task<IEnumerable<Exercise>> ListExercises() => await _repository.ListExercises();

    public async Task Add(Exercise exercise) => await _repository.Add(exercise);

    public async Task<IEnumerable<Exercise>> FindExercisesStartedInTimePeriod(
        DateTime start,
        TimeSpan duration
    ) => await _repository.FindExercisesStartedInTimePeriod(start, duration);
}
