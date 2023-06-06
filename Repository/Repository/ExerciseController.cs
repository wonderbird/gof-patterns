namespace Repository;

public class ExerciseController
{
    private bool _isEmpty = true;

    public IEnumerable<Exercise> ListExercises() =>
        _isEmpty ? Array.Empty<Exercise>() : new[] { new Exercise() };

    public void Add(Exercise _)
    {
        _isEmpty = false;
    }
}
