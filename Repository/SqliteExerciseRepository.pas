unit SqliteExerciseRepository;

interface

uses
  ExerciseRepository, Exercise, Spring, Spring.Collections;

type
  TSqliteExerciseRepository = class(TInterfacedObject, IExerciseRepository)
  public
    procedure Add(Exercise: TExercise);
    function Find(): IEnumerable<TExercise>; overload;
    function Find(const ThePredicate: Predicate<TExercise>):
        IEnumerable<TExercise>; overload;
  end;

implementation

uses
  FireDAC.Comp.Client, FireDAC.Phys.SQLiteDef, SqliteDatabaseConfiguration;

procedure TSqliteExerciseRepository.Add(Exercise: TExercise);
var
  Connection: TFDConnection;
begin
  // TODO -cMM: Ensure that exceptions are processed correctly and Query, Connection are freed even in case of an exception (see similar other todo).
  // TODO -cMM: Reduce code duplication - setting up a connection and destroying it is duplicated.
  Connection := TFDConnection.Create(nil);
  Connection.ConnectionDefName := TSqliteDatabaseConfiguration.ConnectionDefinitionName;
  Connection.Connected := True;

  Connection.ExecSQL('INSERT INTO exercises VALUES(NULL, :start)', [Exercise.Start]);

  Connection.Free;
end;

function TSqliteExerciseRepository.Find: IEnumerable<TExercise>;
var
  Connection: TFDConnection;
  Query: TFDQuery;
  Rows: IList<TExercise>;
  NextStartDate: TDateTime;
  Exercise: TExercise;
begin
  Connection := TFDConnection.Create(nil);
  Connection.ConnectionDefName := TSqliteDatabaseConfiguration.ConnectionDefinitionName;
  Connection.Connected := True;

  // TODO -cMM: Ensure that exceptions are processed correctly and Query, Connection are freed even in case of an exception.
  Query := TFDQuery.Create(nil);
  Query.Connection := Connection;
  Query.SQL.Text := 'SELECT start FROM exercises';
  Query.Open;

  Rows := TCollections.CreateList<TExercise>;
  while not Query.Eof do
  begin
    NextStartDate := Query.FieldByName('start').AsDateTime;
    Exercise.Start := NextStartDate;
    Rows.Add(Exercise);
    Query.Next;
  end;

  Query.Free;
  Connection.Free;

  Result := Rows;
end;

function TSqliteExerciseRepository.Find(const ThePredicate:
    Predicate<TExercise>): IEnumerable<TExercise>;
begin
  Result := nil;
  // TODO -cMM: TSqliteExerciseRepository.Find default body inserted
end;

end.
