namespace Repository;

public class ExerciseController
{
    private readonly List<Exercise> _exercises = new();

    public IEnumerable<Exercise> ListExercises() => _exercises;

    public void Add(Exercise exercise) => _exercises.Add(exercise);
}
