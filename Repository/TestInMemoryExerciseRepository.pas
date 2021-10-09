unit TestInMemoryExerciseRepository;

interface

uses
  DUnitX.TestFramework, InMemoryExerciseRepository;

type

  [TestFixture]
  TestTInMemoryExerciseRepository = class
  private
    FRepository: TInMemoryExerciseRepository;
  public

    [Test]
    procedure Add_GivenFreshRepository_AddsExercise;
    [Test]
    procedure Find_GivenFreshRepository_ReturnsNotNil;
    [Setup]
    procedure Setup;
    [Teardown]
    procedure Teardown;

  end;

implementation

uses
  Exercise, System.Generics.Collections;

procedure TestTInMemoryExerciseRepository.Add_GivenFreshRepository_AddsExercise;
var
  Exercise: TExercise;
  Exercises: TList<TExercise>;
begin
  FRepository.Add(Exercise);
  Exercises := FRepository.Find;
  Assert.AreEqual(1, Exercises.Count, 'invalid number of exercise records');
end;

procedure TestTInMemoryExerciseRepository.
  Find_GivenFreshRepository_ReturnsNotNil;
var
  Exercises: TList<TExercise>;
begin
  Exercises := FRepository.Find;

  Assert.IsTrue(Exercises <> nil, 'nil received');
end;

procedure TestTInMemoryExerciseRepository.Setup;
begin
  FRepository := TInMemoryExerciseRepository.Create;
end;

procedure TestTInMemoryExerciseRepository.Teardown;
begin
  FRepository.Free;
end;

initialization

TDUnitX.RegisterTestFixture(TestTInMemoryExerciseRepository);

end.
