namespace Repository.Tests;

public class ExerciseControllerTests
{
    private readonly DateTime _in2020 = new(2020, 3, 1, 10, 15, 20);
    private readonly DateTime _in2023 = new(2023, 6, 6, 12, 30, 0);
    private readonly TimeSpan _1Year = TimeSpan.FromDays(365);
    private readonly DateTime _startOf2020 = new(2020, 1, 1);
    private readonly ExerciseController _controller;

    public ExerciseControllerTests()
    {
        IRepository repository = new InMemoryRepository();
        _controller = new ExerciseController(repository);
    }

    [Fact]
    public async Task ListExercises_When0ExercisesPresent()
    {
        var actual = await _controller.ListExercises();
        actual.Should().BeEmpty("repository is empty");
    }

    [Fact]
    public async Task ListExercises_When1ExerciseAdded()
    {
        await _controller.Add(new Exercise(_in2023));

        var actual = await _controller.ListExercises();
        actual.Should().Equal(new[] { new Exercise(_in2023) }, "a single exercise was added");
    }

    [Fact]
    public async Task ListExercises_When2ExercisesAdded()
    {
        await _controller.Add(new Exercise(_in2023));
        await _controller.Add(new Exercise(_in2023));

        var actual = await _controller.ListExercises();
        actual
            .Should()
            .Equal(
                new[] { new Exercise(_in2023), new Exercise(_in2023) },
                "two exercises were added"
            );
    }

    [Fact]
    public async Task FindExercisesStartedInTimePeriod_When0ExercisesPresent()
    {
        var actual = await _controller.FindExercisesStartedInTimePeriod(_startOf2020, _1Year);
        actual.Should().BeEmpty("no exercise was started in the given timespan");
    }

    [Fact]
    public async Task FindExercisesStartedInTimePeriod_When1MatchingExercisePresent()
    {
        await _controller.Add(new Exercise(_in2020));

        var actual = await _controller.FindExercisesStartedInTimePeriod(_startOf2020, _1Year);

        actual
            .Should()
            .Equal(
                new[] { new Exercise(_in2020) },
                "a single exercise started in the specified time period"
            );
    }

    [Fact]
    public async Task FindExercisesStartedInTimePeriod_When0MatchingAnd1NonMatchingExercisePresent()
    {
        await _controller.Add(new Exercise(_in2020));
        await _controller.Add(new Exercise(_in2023));

        var actual = await _controller.FindExercisesStartedInTimePeriod(_startOf2020, _1Year);

        actual
            .Should()
            .Equal(
                new[] { new Exercise(_in2020) },
                "only one of two exercises was started in the specified time period"
            );
    }
}
