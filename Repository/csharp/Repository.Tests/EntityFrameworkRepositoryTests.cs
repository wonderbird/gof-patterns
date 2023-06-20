using Testcontainers.PostgreSql;

namespace Repository.Tests;

// ReSharper disable once UnusedType.Global
// because the test framework will instantiate this class.
public class EntityFrameworkRepositoryTests : ExerciseControllerTests, IClassFixture<EntityFrameworkExerciseControllerTestsFixture>
{
    public EntityFrameworkRepositoryTests(EntityFrameworkExerciseControllerTestsFixture fixture)
    {
        var repository = new EntityFrameworkRepository(fixture.ConnectionString);
        Controller = new ExerciseController(repository);
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
// because the test framework will instantiate this class.
public sealed class EntityFrameworkExerciseControllerTestsFixture : IDisposable
{
    private const string UserName = "postgres";

    private const string Password = "example";

    public string ConnectionString => _postgreContainer.GetConnectionString();

    private readonly PostgreSqlContainer _postgreContainer;

    public EntityFrameworkExerciseControllerTestsFixture()
    {
        _postgreContainer = new PostgreSqlBuilder()
            .WithPortBinding(5432, true)
            .WithUsername(UserName)
            .WithPassword(Password)
            .WithCleanUp(true)
            .Build();

        Task.Run(async () => await _postgreContainer.StartAsync()).Wait();
    }

    public void Dispose()
    {
        Task.Run(async () => await _postgreContainer.StopAsync()).Wait();
    }
}
