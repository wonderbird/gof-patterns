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
    procedure Add_GivenFreshRepository_AddsExercise;
    function IsInRange(LowerDateTime: TDateTime; UpperDateTime: TDateTime): Predicate<TExercise>;
    [Test]
    procedure Find_GivenExerciseWithinPeriod_ReturnsExercise;
    [Test]
    procedure Find_GivenExerciseNotWithinPeriod_ReturnsEmptyList;
    [Test]
    procedure Find_GivenFreshRepository_ReturnsNotNil;
    [Setup]
    procedure Setup;
    [Teardown]
    procedure Teardown;

  end;

implementation

uses
  System.DateUtils, Spring.Collections;

procedure TestTInMemoryExerciseRepository.Add_GivenFreshRepository_AddsExercise;
var
  Exercise: TExercise;
  Exercises: IEnumerable<TExercise>;
begin
  FRepository.Add(Exercise);
  Exercises := FRepository.Find;
  Assert.AreEqual(1, Exercises.Count, 'invalid number of exercise records');
end;

function TestTInMemoryExerciseRepository.IsInRange(LowerDateTime: TDateTime; UpperDateTime: TDateTime)
  : Predicate<TExercise>;
begin
  Result := function(const Exercise: TExercise): Boolean
    begin
      Result := (LowerDateTime <= Exercise.Start) and
        (Exercise.Start <= UpperDateTime);
    end;
end;

procedure TestTInMemoryExerciseRepository.
  Find_GivenExerciseWithinPeriod_ReturnsExercise;
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

  Exercises := FRepository.Find(IsInRange(LowerDateTime, UpperDateTime));

  Assert.AreEqual(1, Exercises.Count, 'expected record not found');
end;

procedure TestTInMemoryExerciseRepository.
  Find_GivenExerciseNotWithinPeriod_ReturnsEmptyList;
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

  Exercises := FRepository.Find(IsInRange(LowerDateTime, UpperDateTime));

  Assert.AreEqual(0, Exercises.Count, 'no records expected');
end;

procedure TestTInMemoryExerciseRepository.
  Find_GivenFreshRepository_ReturnsNotNil;
var
  Exercises: IEnumerable<TExercise>;
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
