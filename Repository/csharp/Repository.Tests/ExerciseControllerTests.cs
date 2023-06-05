namespace Repository.Tests;

public class ExerciseControllerTests
{
    [Fact]
    public void List_WhenEmptyRepository()
    {
        var controller = new ExerciseController();
        var actual = controller.ListExercises();
        actual.Should().BeEmpty("repository is empty");
    }
}
