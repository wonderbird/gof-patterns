unit TestInMemoryExerciseRepository;

interface

uses
  Spring, SysUtils, DUnitX.TestFramework, InMemoryExerciseRepository, Exercise;

type

  [TestFixture]
  TestTInMemoryExerciseRepository = class
  private
    FRepository: TInMemoryExerciseRepository;
  public

    [Test]
    procedure Add_GivenFreshRepository_ThenAddsExercise;
    [Test]
    procedure Find_GivenExerciseWithinPeriod_ThenReturnsExercise;
    [Test]
    procedure Find_GivenExerciseNotWithinPeriod_ThenReturnsEmptyList;
    [Test]
    procedure Find_GivenFreshRepository_ThenReturns0Exercises;
    [Setup]
    procedure Setup;
    [Teardown]
    procedure Teardown;

  end;

implementation

uses
  System.DateUtils, Spring.Collections, Predicates;

procedure TestTInMemoryExerciseRepository.Add_GivenFreshRepository_ThenAddsExercise;
var
  Exercise: TExercise;
  Exercises: IEnumerable<TExercise>;
begin
  FRepository.Add(Exercise);
  Exercises := FRepository.Find;
  Assert.AreEqual(1, Exercises.Count, 'invalid number of exercise records');
end;

procedure TestTInMemoryExerciseRepository.
  Find_GivenExerciseWithinPeriod_ThenReturnsExercise;
var
  Exercise: TExercise;
  Exercises: IEnumerable<TExercise>;
  LowerDateTime: TDateTime;
  UpperDateTime: TDateTime;
begin
  Exercise.Start := EncodeDateTime(2021, 10, 8, 8, 0, 0, 0);
  FRepository.Add(Exercise);

  LowerDateTime := EncodeDateTime(2021, 10, 8, 0, 0, 0, 0);
  UpperDateTime := EncodeDateTime(2021, 10, 9, 0, 0, 0, 0);

  Exercises := FRepository.Find(TPredicates.IsInRange(LowerDateTime, UpperDateTime));

  Assert.AreEqual(1, Exercises.Count, 'expected record not found');
end;

procedure TestTInMemoryExerciseRepository.
  Find_GivenExerciseNotWithinPeriod_ThenReturnsEmptyList;
var
  Exercise: TExercise;
  Exercises: IEnumerable<TExercise>;
  LowerDateTime: TDateTime;
  UpperDateTime: TDateTime;
begin
  Exercise.Start := EncodeDateTime(1970, 10, 8, 8, 0, 0, 0);
  FRepository.Add(Exercise);

  LowerDateTime := EncodeDateTime(2021, 10, 8, 0, 0, 0, 0);
  UpperDateTime := EncodeDateTime(2021, 10, 9, 0, 0, 0, 0);

  Exercises := FRepository.Find(TPredicates.IsInRange(LowerDateTime, UpperDateTime));

  Assert.AreEqual(0, Exercises.Count, 'no records expected');
end;

procedure TestTInMemoryExerciseRepository.
  Find_GivenFreshRepository_ThenReturns0Exercises;
var
  Exercises: IEnumerable<TExercise>;
begin
  Exercises := FRepository.Find;

  Assert.AreEqual(0, Exercises.Count);
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
