unit TestExerciseController;

interface

uses
  Spring.Collections, DUnitX.TestFramework, ExerciseController, ExerciseRepository, Exercise;

type

  [TestFixture]
  TestTExerciseController = class
  private
    FController: TExerciseController;
    FExercise: TExercise;
    FExercises: IEnumerable<TExercise>;
    FRepository: IExerciseRepository;
  public
    [Test]
    procedure Add_GivenEmptyRepository_AddsExercise;
    [Test]
    procedure FindExercisesStartedInTimePeriod_GivenRepositoryContainsMatchingExercises_ThenReturnsThatExercise;
    [Test]
    procedure ListExercises_GivenRepositoryWithExercises_ThenReturnsNotNil;
    [Setup]
    procedure Setup;
    [Teardown]
    procedure Teardown;
  end;

implementation

uses
  InMemoryExerciseRepository, System.DateUtils;

procedure TestTExerciseController.Add_GivenEmptyRepository_AddsExercise;
begin
  FController.AddExercise(FExercise);
  FExercises := FRepository.Find;
  Assert.AreEqual(1, FExercises.Count, 'unexpected number of exercises')
end;

procedure TestTExerciseController.
  FindExercisesStartedInTimePeriod_GivenRepositoryContainsMatchingExercises_ThenReturnsThatExercise;
var
  LowerDateTime: TDateTime;
  UpperDateTime: TDateTime;
begin
  FExercise.Start := EncodeDateTime(1970, 10, 8, 8, 0, 0, 0);
  FRepository.Add(FExercise);
  FExercise.Start := EncodeDateTime(2021, 10, 8, 8, 0, 0, 0);
  FRepository.Add(FExercise);

  LowerDateTime := EncodeDateTime(2021, 10, 8, 1, 0, 0, 0);
  UpperDateTime := EncodeDateTime(2021, 10, 8, 23, 59, 59, 0);
  FExercises := FController.FindExercisesStartedInTimePeriod(LowerDateTime,
    UpperDateTime);

  Assert.AreEqual(1, FExercises.Count, 'invalid number of exercises');
end;

procedure TestTExerciseController.
  ListExercises_GivenRepositoryWithExercises_ThenReturnsNotNil;
begin
  FRepository.Add(FExercise);

  FExercises := FController.ListExercises;

  Assert.AreEqual(1, FExercises.Count, 'invalid number of exercises');
end;

procedure TestTExerciseController.Setup;
begin
  FRepository := TInMemoryExerciseRepository.Create;
  FController := TExerciseController.Create(FRepository);
end;

procedure TestTExerciseController.Teardown;
begin
  FController.Free;
end;

initialization

TDUnitX.RegisterTestFixture(TestTExerciseController);

end.
