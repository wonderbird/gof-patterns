unit SqliteExerciseRepository;

interface

uses
  ExerciseRepository, Exercise, Spring, Spring.Collections, FireDAC.Comp.Client;

type
  TSqliteExerciseRepository = class(TInterfacedObject, IExerciseRepository)
  private
    function CreateActiveDbConnection: TFDConnection;
  public
    procedure Add(Exercise: TExercise);
    function Find(): IEnumerable<TExercise>; overload;
    function Find(const ThePredicate: Predicate<TExercise>)
      : IEnumerable<TExercise>; overload;
  end;

implementation

uses
  FireDAC.Phys.SQLiteDef, SqliteDatabaseConfiguration;

procedure TSqliteExerciseRepository.Add(Exercise: TExercise);
var
  Connection: TFDConnection;
begin
  Connection := CreateActiveDbConnection;

  try
    Connection.ExecSQL('INSERT INTO exercises VALUES(NULL, :start)',
      [Exercise.Start]);
  finally
    Connection.Free;
  end;
end;

function TSqliteExerciseRepository.CreateActiveDbConnection: TFDConnection;
var
  Connection: TFDConnection;
begin
  Connection := TFDConnection.Create(nil);
  Connection.ConnectionDefName :=
    TSqliteDatabaseConfiguration.ConnectionDefinitionName;
  Connection.Connected := True;
  Result := Connection;
end;

function TSqliteExerciseRepository.Find: IEnumerable<TExercise>;
var
  Connection: TFDConnection;
  Query: TFDQuery;
  Rows: IList<TExercise>;
  NextStartDate: TDateTime;
  Exercise: TExercise;
begin
  Connection := CreateActiveDbConnection;
  Query := TFDQuery.Create(nil);
  Rows := TCollections.CreateList<TExercise>;

  try
    Query.Connection := Connection;
    Query.SQL.Text := 'SELECT start FROM exercises';
    Query.Open;

    while not Query.Eof do
    begin
      NextStartDate := Query.FieldByName('start').AsDateTime;
      Exercise.Start := NextStartDate;
      Rows.Add(Exercise);
      Query.Next;
    end;
  finally
    Query.Free;
    Connection.Free;
  end;

  Result := Rows;
end;

function TSqliteExerciseRepository.Find(const ThePredicate
  : Predicate<TExercise>): IEnumerable<TExercise>;
begin
  Result := nil;
  // TODO -cMM: TSqliteExerciseRepository.Find default body inserted
end;

end.
