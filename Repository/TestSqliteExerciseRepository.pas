unit TestSqliteExerciseRepository;

interface

uses
  DUnitX.TestFramework, FireDAC.Comp.Client;

type

  [TestFixture]
  TestTSqliteExerciseRepository = class
  private
    FConnection: TFDConnection;
  public
    [Test]
    [Ignore('Not implemented yet.')]
    procedure Add;
    [Test]
    procedure Find_EmptyRepository_ReturnsEmptyCollection;
    [Test]
    procedure Find_2EntriesInDatabase_ReturnsCollectionWith2Entries;
    procedure InitializeExercisesTable;
    [Setup]
    procedure Setup;

  end;

implementation

uses
  FireDAC.Phys.SQLiteDef, SqliteDatabaseConfiguration,
  Spring.Collections, ExerciseRepository, Exercise, SqliteExerciseRepository,
  System.SysUtils;

procedure TestTSqliteExerciseRepository.Add;
var
  Exercise: TExercise;
  Repository: IExerciseRepository;
  Rows: IEnumerable<TExercise>;
begin
  Repository := TSqliteExerciseRepository.Create();
  Repository.Add(Exercise);
  Rows := Repository.Find;

  Assert.AreEqual(1, Rows.Count, 'unexpected number of rows');
end;

procedure TestTSqliteExerciseRepository.Find_EmptyRepository_ReturnsEmptyCollection;
var
  Repository: IExerciseRepository;
  Rows: IEnumerable<TExercise>;
begin
  Repository := TSqliteExerciseRepository.Create();
  Rows := Repository.Find;

  Assert.AreEqual(0, Rows.Count, 'unexpected number of rows');
end;

procedure
    TestTSqliteExerciseRepository.Find_2EntriesInDatabase_ReturnsCollectionWith2Entries;
var
  Repository: IExerciseRepository;
  Rows: IEnumerable<TExercise>;
begin
  FConnection.ExecSQL('INSERT INTO exercises VALUES (NULL, ''2021-10-08T07:00:00.000Z'')');
  FConnection.ExecSQL('INSERT INTO exercises VALUES (NULL, ''2021-10-09T07:00:00.000Z'')');

  Repository := TSqliteExerciseRepository.Create();
  Rows := Repository.Find;

  Assert.AreEqual(2, Rows.Count, 'unexpected number of rows');
end;

procedure TestTSqliteExerciseRepository.InitializeExercisesTable;
begin
  FConnection := TFDConnection.Create(nil);
  FConnection.ConnectionDefName := TSqliteDatabaseConfiguration.ConnectionDefinitionName;
  FConnection.Connected := True;

  FConnection.ExecSQL('CREATE TABLE IF NOT EXISTS exercises (id INTEGER PRIMARY KEY AUTOINCREMENT, start DATETIME)');
  FConnection.ExecSQL('DELETE FROM exercises');
end;

procedure TestTSqliteExerciseRepository.Setup;
begin
  InitializeExercisesTable;
end;

initialization

TDUnitX.RegisterTestFixture(TestTSqliteExerciseRepository);

end.
