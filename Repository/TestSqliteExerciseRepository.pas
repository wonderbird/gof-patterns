unit TestSqliteExerciseRepository;

interface

uses
  DUnitX.TestFramework;

type

  [TestFixture]
  TestTSqliteExerciseRepository = class
  public
    [Test]
    [Ignore('Not implemented yet.')]
    procedure Add;
  end;

implementation

uses
  Spring.Collections, ExerciseRepository, Exercise, SqliteExerciseRepository;

procedure TestTSqliteExerciseRepository.Add;
var
  Exercise: TExercise;
  Repository: IExerciseRepository;
  Rows: IList<TDateTime>;
begin
  Repository := TSqliteExerciseRepository.Create();
  Rows := TCollections.CreateList<TDateTime>;
  Repository.Add(Exercise);

  Assert.AreEqual(1, Rows.Count, 'unexpected number of rows');
end;

initialization

TDUnitX.RegisterTestFixture(TestTSqliteExerciseRepository);

end.
