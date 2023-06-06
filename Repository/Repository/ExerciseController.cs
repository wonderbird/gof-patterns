namespace Repository;

public class ExerciseController
{
    private readonly List<Exercise> _exercises = new();

    public IEnumerable<Exercise> ListExercises() => _exercises;

    public void Add(Exercise exercise) => _exercises.Add(exercise);

    public IEnumerable<Exercise> FindExercisesStartedInTimePeriod(DateTime start, TimeSpan duration)
    {
        var end = start + duration;
        return _exercises.Where(e => e.Start >= start && e.Start <= end);
    }
}
