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

    [Fact]
    public void FindExercisesStartedInTimePeriod_When0ExercisesPresent()
    {
        var actual = _controller.FindExercisesStartedInTimePeriod(
            new DateTime(2020, 1, 1),
            TimeSpan.FromDays(7.0)
        );
        actual.Should().BeEmpty("no exercise was started in the given timespan");
    }

    [Fact]
    public void FindExercisesStartedInTimePeriod_When1MatchingExercisePresent()
    {
        _controller.Add(new Exercise());
        var actual = _controller.FindExercisesStartedInTimePeriod(
            new DateTime(2020, 1, 1),
            TimeSpan.FromDays(7.0)
        );
        actual
            .Should()
            .Equal(
                new[] { new Exercise() },
                "a single exercise started in the specified time period"
            );
    }
}
