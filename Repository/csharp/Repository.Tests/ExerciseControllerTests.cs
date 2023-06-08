using System.Diagnostics.CodeAnalysis;

namespace Repository.Tests;

public class ExerciseControllerTests
{
    private readonly DateTime _in2020 = new(2020, 3, 1, 10, 15, 20);
    private readonly DateTime _in2023 = new(2023, 6, 6, 12, 30, 0);
    private readonly TimeSpan _1Year = TimeSpan.FromDays(365);
    private readonly DateTime _startOf2020 = new(2020, 1, 1);

    private class Repositories : TheoryData<IRepository>
    {
        [SuppressMessage(
            "Sonar Code Smell",
            "S1144:Unused private types or members should be removed",
            Justification = "Repositories class is instantiated by xUnit for each Theory"
        )]
        public Repositories()
        {
            Add(new InMemoryRepository());
        }
    }

    [Theory]
    [ClassData(typeof(Repositories))]
    public void ListExercises_When0ExercisesPresent(IRepository repository)
    {
        var controller = new ExerciseController(repository);
        var actual = controller.ListExercises();
        actual.Should().BeEmpty("repository is empty");
    }

    [Theory]
    [ClassData(typeof(Repositories))]
    public void ListExercises_When1ExerciseAdded(IRepository repository)
    {
        var controller = new ExerciseController(repository);
        controller.Add(new Exercise(_in2023));

        var actual = controller.ListExercises();
        actual.Should().Equal(new[] { new Exercise(_in2023) }, "a single exercise was added");
    }

    [Theory]
    [ClassData(typeof(Repositories))]
    public void ListExercises_When2ExercisesAdded(IRepository repository)
    {
        var controller = new ExerciseController(repository);
        controller.Add(new Exercise(_in2023));
        controller.Add(new Exercise(_in2023));

        var actual = controller.ListExercises();
        actual
            .Should()
            .Equal(
                new[] { new Exercise(_in2023), new Exercise(_in2023) },
                "two exercises were added"
            );
    }

    [Theory]
    [ClassData(typeof(Repositories))]
    public void FindExercisesStartedInTimePeriod_When0ExercisesPresent(IRepository repository)
    {
        var controller = new ExerciseController(repository);
        var actual = controller.FindExercisesStartedInTimePeriod(_startOf2020, _1Year);
        actual.Should().BeEmpty("no exercise was started in the given timespan");
    }

    [Theory]
    [ClassData(typeof(Repositories))]
    public void FindExercisesStartedInTimePeriod_When1MatchingExercisePresent(
        IRepository repository
    )
    {
        var controller = new ExerciseController(repository);
        controller.Add(new Exercise(_in2020));

        var actual = controller.FindExercisesStartedInTimePeriod(_startOf2020, _1Year);

        actual
            .Should()
            .Equal(
                new[] { new Exercise(_in2020) },
                "a single exercise started in the specified time period"
            );
    }

    [Theory]
    [ClassData(typeof(Repositories))]
    public void FindExercisesStartedInTimePeriod_When0MatchingAnd1NonMatchingExercisePresent(
        IRepository repository
    )
    {
        var controller = new ExerciseController(repository);
        controller.Add(new Exercise(_in2020));
        controller.Add(new Exercise(_in2023));

        var actual = controller.FindExercisesStartedInTimePeriod(_startOf2020, _1Year);

        actual
            .Should()
            .Equal(
                new[] { new Exercise(_in2020) },
                "only one of two exercises was started in the specified time period"
            );
    }
}
