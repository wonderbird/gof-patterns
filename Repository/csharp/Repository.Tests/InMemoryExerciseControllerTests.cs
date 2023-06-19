namespace Repository.Tests;

// ReSharper disable once UnusedType.Global
// because the test framework will instantiate this class.
public class InMemoryExerciseControllerTests : ExerciseControllerTests
{
    public InMemoryExerciseControllerTests() =>
        Controller = new ExerciseController(new InMemoryRepository());
}
