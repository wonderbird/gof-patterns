namespace Repository.Tests;

public class ExerciseControllerTests
{
    private readonly ExerciseController _controller;
    private readonly DateTime _in2020 = new(2020, 3, 1, 10, 15, 20);
    private readonly DateTime _in2023 = new(2023, 6, 6, 12, 30, 0);
    private readonly TimeSpan _1Year = TimeSpan.FromDays(365);
    private readonly DateTime _startOf2020 = new(2020, 1, 1);

    public ExerciseControllerTests() =>
        _controller = new ExerciseController(new InMemoryRepository());

    [Fact]
    public void ListExercises_When0ExercisesPresent()
    {
        var actual = _controller.ListExercises();
        actual.Should().BeEmpty("repository is empty");
    }

    [Fact]
    public void ListExercises_When1ExerciseAdded()
    {
        _controller.Add(new Exercise(_in2023));

        var actual = _controller.ListExercises();
        actual.Should().Equal(new[] { new Exercise(_in2023) }, "a single exercise was added");
    }

    [Fact]
    public void ListExercises_When2ExercisesAdded()
    {
        _controller.Add(new Exercise(_in2023));
        _controller.Add(new Exercise(_in2023));

        var actual = _controller.ListExercises();
        actual
            .Should()
            .Equal(
                new[] { new Exercise(_in2023), new Exercise(_in2023) },
                "two exercises were added"
            );
    }

    [Fact]
    public void FindExercisesStartedInTimePeriod_When0ExercisesPresent()
    {
        var actual = _controller.FindExercisesStartedInTimePeriod(_startOf2020, _1Year);
        actual.Should().BeEmpty("no exercise was started in the given timespan");
    }

    [Fact]
    public void FindExercisesStartedInTimePeriod_When1MatchingExercisePresent()
    {
        _controller.Add(new Exercise(_in2020));

        var actual = _controller.FindExercisesStartedInTimePeriod(_startOf2020, _1Year);

        actual
            .Should()
            .Equal(
                new[] { new Exercise(_in2020) },
                "a single exercise started in the specified time period"
            );
    }

    [Fact]
    public void FindExercisesStartedInTimePeriod_When0MatchingAnd1NonMatchingExercisePresent()
    {
        _controller.Add(new Exercise(_in2020));
        _controller.Add(new Exercise(_in2023));

        var actual = _controller.FindExercisesStartedInTimePeriod(_startOf2020, _1Year);

        actual
            .Should()
            .Equal(
                new[] { new Exercise(_in2020) },
                "only one of two exercises was started in the specified time period"
            );
    }
}
