namespace Repository.Tests;

public class ExerciseControllerTests
{
    private readonly ExerciseController _controller;

    public ExerciseControllerTests() => _controller = new ExerciseController();

    [Fact]
    public void ListExercises_When0ExercisesPresent()
    {
        var actual = _controller.ListExercises();
        actual.Should().BeEmpty("repository is empty");
    }

    [Fact]
    public void ListExercises_When1ExerciseAdded()
    {
        _controller.Add(new Exercise());

        var actual = _controller.ListExercises();
        actual.Should().Equal(new[] { new Exercise() }, "a single exercise was added");
    }

    [Fact]
    public void ListExercises_When2ExercisesAdded()
    {
        _controller.Add(new Exercise());
        _controller.Add(new Exercise());

        var actual = _controller.ListExercises();
        actual.Should().Equal(new[] { new Exercise(), new Exercise() }, "two exercises were added");
    }

    // TODO: Boundary Test - modifying the returned list may not alter the list returned by another call of the controller
}
