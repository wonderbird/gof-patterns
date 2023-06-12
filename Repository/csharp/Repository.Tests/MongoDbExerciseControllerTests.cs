using MongoDB.Driver;
using Testcontainers.MongoDb;

namespace Repository.Tests;

// ReSharper disable once ClassNeverInstantiated.Global
// because the test framework will create an instance.
public sealed class MongoDbExerciseControllerTestsFixture : IDisposable
{
    private const string UserName = "root";

    private const string Password = "example";

    public string ConnectionString => _mongoContainer.GetConnectionString();

    private readonly MongoDbContainer _mongoContainer;

    public MongoDbExerciseControllerTestsFixture()
    {
        _mongoContainer = new MongoDbBuilder()
            .WithPortBinding(27017, true)
            .WithUsername(UserName)
            .WithPassword(Password)
            .WithCleanUp(true)
            .Build();

        Task.Run(async () => await _mongoContainer.StartAsync()).Wait();
    }

    public void Dispose()
    {
        Task.Run(async () => await _mongoContainer.StopAsync()).Wait();
    }
}

public class MongoDbExerciseControllerTests : IClassFixture<MongoDbExerciseControllerTestsFixture>
{
    private readonly DateTime _in2020 = new(2020, 3, 1, 10, 15, 20);
    private readonly DateTime _in2023 = new(2023, 6, 6, 12, 30, 0);
    private readonly TimeSpan _1Year = TimeSpan.FromDays(365);
    private readonly DateTime _startOf2020 = new(2020, 1, 1);
    private readonly ExerciseController _controller;

    public MongoDbExerciseControllerTests(MongoDbExerciseControllerTestsFixture fixture)
    {
        var mongoClient  = new MongoClient(fixture.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase("exercises");
        mongoDatabase.DropCollection("Exercises");

        var repository = new MongoDbRepository(fixture.ConnectionString);
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
