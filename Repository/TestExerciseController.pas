unit TestExerciseController;

interface

uses
  DUnitX.TestFramework;

type
  [TestFixture]
  TestTExerciseController = class
  public
    [Test]
    procedure ListExercises_GivenRepositoryWithExercises_ThenReturnsNotNil;
  end;

implementation

uses
  Exercise, ExerciseController, System.Generics.Collections,
  InMemoryExerciseRepository, ExerciseRepository;

procedure
    TestTExerciseController.ListExercises_GivenRepositoryWithExercises_ThenReturnsNotNil;
var
  Controller: TExerciseController;
  Exercise: TExercise;
  Exercises: TList<TExercise>;
  Repository: IExerciseRepository;
begin
  Repository := TInMemoryExerciseRepository.Create;
  Repository.Add(Exercise);

  Controller := TExerciseController.Create(Repository);
  try
    Exercises := Controller.ListExercises;

    Assert.AreEqual(1, Exercises.Count, 'invalid number of exercises');
  finally
    Controller.Free;
  end;
end;

initialization
  TDUnitX.RegisterTestFixture(TestTExerciseController);

end.
