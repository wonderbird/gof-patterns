unit TestCsvFileExerciseRepository;

interface

uses
  Spring.Collections, Exercise, DUnitX.TestFramework, CsvFileExerciseRepository;

type

  [TestFixture]
  TestTCsvFileExerciseRepository = class
  private
    FExercises: IEnumerable<TExercise>;
    FRepository: TCsvFileExerciseRepository;
    FTestDataDirectory: String;
  public
    [Test]
    procedure Add_GivenEmptyFile_ThenFileContainsAddedLine;
    [Test]
    procedure Find_GivenFileContains5Exercises_ThenReturns5ExercisesWithCorrectTimestamp;
    [Test]
    procedure Find_GivenFileDoesNotExist_ThenReturns0Exercises;
    [Setup]
    procedure Setup;
    [Teardown]
    procedure Teardown;
  end;

implementation

uses
  System.DateUtils, System.Classes, System.SysUtils, System.IOUtils;

procedure
    TestTCsvFileExerciseRepository.Add_GivenEmptyFile_ThenFileContainsAddedLine;
var
  Exercise: TExercise;
  FilePath: string;
  Lines: TStringList;
  MyClass: TComponent;
begin
  FilePath := TPath.Combine(FTestDataDirectory,'temporary-file.csv');
  Lines := TStringList.Create;
  try
    DeleteFile(FilePath);
    FRepository.SetFilePath(FilePath);

    FRepository.Add(Exercise);

    Lines.LoadFromFile(FilePath);
    Assert.AreEqual(2, Lines.Count, 'unexpected number of lines in file');
  finally
    Lines.Free;
  end;
end;

procedure TestTCsvFileExerciseRepository.
  Find_GivenFileContains5Exercises_ThenReturns5ExercisesWithCorrectTimestamp;
var
  Exercise: TExercise;
  ExpectedExercises: IList<TExercise>;
  FilePath: string;
begin
  FilePath := TPath.Combine(FTestDataDirectory, 'repository-with-5-entries.csv');
  FRepository.SetFilePath(FilePath);
  FExercises := FRepository.Find;
  Assert.AreEqual(5, FExercises.Count, 'unexpected number of records in file');

  ExpectedExercises := TCollections.CreateList<TExercise>;
  Exercise.Start := EncodeDateTime(2021, 10, 1, 8, 30, 0, 0);
  ExpectedExercises.Add(Exercise);
  Exercise.Start := EncodeDateTime(2021, 10, 2, 10, 30, 0, 0);
  ExpectedExercises.Add(Exercise);
  Exercise.Start := EncodeDateTime(2021, 10, 3, 12, 30, 0, 0);
  ExpectedExercises.Add(Exercise);
  Exercise.Start := EncodeDateTime(2021, 10, 4, 14, 30, 0, 0);
  ExpectedExercises.Add(Exercise);
  Exercise.Start := EncodeDateTime(2021, 10, 5, 16, 30, 0, 0);
  ExpectedExercises.Add(Exercise);

  Assert.AreEqual(ExpectedExercises.ToArray, FExercises.ToArray, 'start dates do not match');

end;

procedure
    TestTCsvFileExerciseRepository.Find_GivenFileDoesNotExist_ThenReturns0Exercises;
var
  Exercises: IEnumerable<TExercise>;
begin
  FRepository.SetFilePath('this-file-should-not-exist.csv');
  Exercises := FRepository.Find;
  Assert.AreEqual(0, ExerCises.Count);
end;

procedure TestTCsvFileExerciseRepository.Setup;
begin
  FTestDataDirectory := TPath.Combine(TPath.Combine('..', '..'), 'test-data');
  FRepository := TCsvFileExerciseRepository.Create;
end;

procedure TestTCsvFileExerciseRepository.Teardown;
begin
  FRepository.Free;
end;

initialization

TDUnitX.RegisterTestFixture(TestTCsvFileExerciseRepository);

end.
