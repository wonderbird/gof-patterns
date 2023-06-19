using MongoDB.Driver;
using Testcontainers.MongoDb;

namespace Repository.Tests;

// ReSharper disable once UnusedType.Global
// because the test framework will instantiate this class.
public class MongoDbExerciseControllerTests : ExerciseControllerTests,
    IClassFixture<MongoDbExerciseControllerTestsFixture>
{
    public MongoDbExerciseControllerTests(MongoDbExerciseControllerTestsFixture fixture)
    {
        var mongoClient = new MongoClient(fixture.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase("exercises");
        mongoDatabase.DropCollection("Exercises");

        var repository = new MongoDbRepository(fixture.ConnectionString);
        Controller = new ExerciseController(repository);
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
// because the test framework will instantiate this class.
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
