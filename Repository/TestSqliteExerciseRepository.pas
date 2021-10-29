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

    [Test]
    procedure Find_EmptyRepository_ReturnsEmptyCollection;
    [Test]
    procedure Find_2EntriesInDatabase_ReturnsCollectionWith2Entries;

  end;

implementation

uses
  FireDAC.Comp.Client, FireDAC.Phys.SQLiteDef, SqliteDatabaseConfiguration,
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
  Connection: TFDConnection;
  Repository: IExerciseRepository;
  Rows: IEnumerable<TExercise>;
begin
  DeleteFile(TSqliteDatabaseConfiguration.DatabaseFileName);

  Connection := TFDConnection.Create(nil);
  Connection.ConnectionDefName := TSqliteDatabaseConfiguration.ConnectionDefinitionName;
  Connection.Connected := True;

  Connection.ExecSQL('CREATE TABLE IF NOT EXISTS exercises (id INTEGER PRIMARY KEY AUTOINCREMENT, start DATETIME)');
  Connection.ExecSQL('INSERT INTO exercises VALUES (NULL, ''2021-10-08T07:00:00.000Z'')');
  Connection.ExecSQL('INSERT INTO exercises VALUES (NULL, ''2021-10-09T07:00:00.000Z'')');

  Repository := TSqliteExerciseRepository.Create();
  Rows := Repository.Find;

  Assert.AreEqual(2, Rows.Count, 'unexpected number of rows');
end;

initialization

TDUnitX.RegisterTestFixture(TestTSqliteExerciseRepository);

end.
