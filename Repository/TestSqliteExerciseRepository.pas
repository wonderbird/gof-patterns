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
    procedure Add_DatabaseEmptyBefore_InsertsRowCorrectly;
    [Test]
    procedure Find_EmptyDatabase_ReturnsEmptyCollection;
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

procedure TestTSqliteExerciseRepository.Add_DatabaseEmptyBefore_InsertsRowCorrectly;
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

procedure TestTSqliteExerciseRepository.Find_EmptyDatabase_ReturnsEmptyCollection;
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
