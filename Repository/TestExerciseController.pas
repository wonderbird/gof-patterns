unit TestExerciseController;

interface

uses
  Spring.Collections, DUnitX.TestFramework, ExerciseController, ExerciseRepository, Exercise;

type

  [TestFixture]
  TestTExerciseController<TRepositoryType : IExerciseRepository, constructor> = class
  private
    FController: TExerciseController;
    FExercise: TExercise;
    FExercises: IEnumerable<TExercise>;
    FRepository: IExerciseRepository;
  public
    [Test]
    procedure Add_GivenEmptyRepository_ThenAddsExerciseToRepository;
    [Test]
    procedure FindExercisesStartedInTimePeriod_GivenRepositoryContainsMatchingExercises_ThenReturnsThatExercise;
    [Test]
    procedure ListExercises_GivenRepositoryWithOneExercise_ThenReturnsThatExercise;
    [Setup]
    procedure Setup;
    [Teardown]
    procedure Teardown;
  end;

implementation

uses
  InMemoryExerciseRepository, CsvFileExerciseRepository, System.DateUtils, System.SysUtils;

procedure TestTExerciseController<TRepositoryType>.Add_GivenEmptyRepository_ThenAddsExerciseToRepository;
begin
  FController.AddExercise(FExercise);
  FExercises := FRepository.Find;
  Assert.AreEqual(1, FExercises.Count, 'unexpected number of exercises')
end;

procedure TestTExerciseController<TRepositoryType>.
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

procedure TestTExerciseController<TRepositoryType>.
  ListExercises_GivenRepositoryWithOneExercise_ThenReturnsThatExercise;
begin
  FRepository.Add(FExercise);

  FExercises := FController.ListExercises;

  Assert.AreEqual(1, FExercises.Count, 'invalid number of exercises');
end;

procedure TestTExerciseController<TRepositoryType>.Setup;
begin
  DeleteFile(TCsvFileExerciseRepository.DefaultFilePath);
  FRepository := TRepositoryType.Create;
  FController := TExerciseController.Create(FRepository);
end;

procedure TestTExerciseController<TRepositoryType>.Teardown;
begin
  FController.Free;
end;

initialization

TDUnitX.RegisterTestFixture(TestTExerciseController<TInMemoryExerciseRepository>);
TDUnitX.RegisterTestFixture(TestTExerciseController<TCsvFileExerciseRepository>);

end.
