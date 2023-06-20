using Npgsql;
using Testcontainers.PostgreSql;

namespace Repository.Tests;

// ReSharper disable once UnusedType.Global
// because the test framework will instantiate this class.
public class EntityFrameworkRepositoryTests : ExerciseControllerTests, IClassFixture<EntityFrameworkExerciseControllerTestsFixture>
{
    public EntityFrameworkRepositoryTests(EntityFrameworkExerciseControllerTestsFixture fixture)
    {
        const string sqlText = @"
DROP TABLE IF EXISTS ""Exercises"";
CREATE TABLE ""Exercises"" (
    ""Id"" bigint NOT NULL,
    ""Start"" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    CONSTRAINT ""Exercises_PrimaryKey"" PRIMARY KEY (""Id"")
);
";

        Task.Run(async () =>
        {
            await using var dataSource = NpgsqlDataSource.Create(fixture.ConnectionString);
            await using var command = dataSource.CreateCommand(sqlText);
            await command.ExecuteNonQueryAsync();
        }).Wait();

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
