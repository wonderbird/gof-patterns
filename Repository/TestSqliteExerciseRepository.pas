unit TestSqliteExerciseRepository;

interface

uses
  Exercise, DUnitX.TestFramework, FireDAC.Comp.Client, Spring;

type

  [TestFixture]
  TestTSqliteExerciseRepository = class
  private
    FConnection: TFDConnection;
  public
    [Test]
    procedure Add_GivenDatabaseEmptyBefore_ThenInsertsRowCorrectly;
    [Test]
    procedure Find_Given2EntriesInDatabase_ThenReturnsCollectionWith2Entries;
    [Test]
    procedure Find_Given3EntriesInDatabaseAnd1MatchesPredicate_ThenReturnsCollectionWith1Entry;
    [Test]
    procedure Find_GivenEmptyDatabase_ThenReturnsEmptyCollection;
    [Setup]
    procedure Setup;
  end;

implementation

uses
  FireDAC.Phys.SQLiteDef, SqliteDatabaseConfiguration,
  Spring.Collections, ExerciseRepository, SqliteExerciseRepository,
  System.DateUtils, System.SysUtils, Predicates;

procedure TestTSqliteExerciseRepository.
  Add_GivenDatabaseEmptyBefore_ThenInsertsRowCorrectly;
var
  Exercise: TExercise;
  Repository: IExerciseRepository;
  RowCount: Integer;
begin
  Repository := TSqliteExerciseRepository.Create();
  Repository.Add(Exercise);

  RowCount := FConnection.ExecSQLScalar('SELECT COUNT(*) FROM exercises');

  Assert.AreEqual(1, RowCount, 'unexpected number of rows');
end;

procedure TestTSqliteExerciseRepository.
  Find_Given2EntriesInDatabase_ThenReturnsCollectionWith2Entries;
var
  Repository: IExerciseRepository;
  Rows: IEnumerable<TExercise>;
begin
  FConnection.ExecSQL
    ('INSERT INTO exercises VALUES (NULL, ''2021-10-08T07:00:00.000Z'')');
  FConnection.ExecSQL
    ('INSERT INTO exercises VALUES (NULL, ''2021-10-09T07:00:00.000Z'')');

  Repository := TSqliteExerciseRepository.Create();
  Rows := Repository.Find;

  Assert.AreEqual(2, Rows.Count, 'unexpected number of rows');
end;

procedure TestTSqliteExerciseRepository.
  Find_Given3EntriesInDatabaseAnd1MatchesPredicate_ThenReturnsCollectionWith1Entry;
var
  LowerDateTime: TDateTime;
  Repository: TSqliteExerciseRepository;
  Rows: IEnumerable<TExercise>;
  UpperDateTime: TDateTime;
begin
  FConnection.ExecSQL
    ('INSERT INTO exercises VALUES (NULL, ''2021-10-08T07:00:00.000Z'')');
  FConnection.ExecSQL
    ('INSERT INTO exercises VALUES (NULL, ''2021-10-09T07:00:00.000Z'')');
  FConnection.ExecSQL
    ('INSERT INTO exercises VALUES (NULL, ''2021-10-10T07:00:00.000Z'')');

  LowerDateTime := EncodeDateTime(2021, 10, 8, 0, 0, 0, 0);
  UpperDateTime := EncodeDateTime(2021, 10, 9, 0, 0, 0, 0);

  Repository := TSqliteExerciseRepository.Create();
  Rows := Repository.Find(TPredicates.IsInRange(LowerDateTime, UpperDateTime));

  Assert.AreEqual(1, Rows.Count, 'unexpected number of rows');
end;

procedure TestTSqliteExerciseRepository.
  Find_GivenEmptyDatabase_ThenReturnsEmptyCollection;
var
  Repository: IExerciseRepository;
  Rows: IEnumerable<TExercise>;
begin
  Repository := TSqliteExerciseRepository.Create();
  Rows := Repository.Find;

  Assert.AreEqual(0, Rows.Count, 'unexpected number of rows');
end;

procedure TestTSqliteExerciseRepository.Setup;
begin
  TSqliteExerciseRepository.DeleteAllExercises;

  FConnection := TFDConnection.Create(nil);
  FConnection.ConnectionDefName :=
    TSqliteDatabaseConfiguration.ConnectionDefinitionName;
  FConnection.Connected := True;
end;

initialization

TDUnitX.RegisterTestFixture(TestTSqliteExerciseRepository);

end.
