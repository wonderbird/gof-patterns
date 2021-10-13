unit TestUsingFireDAC;

interface

uses
  DUnitX.TestFramework, System.IOUtils;

type

  [TestFixture]
  TestTUsingFireDAC = class
  public
    [Test]
    procedure WhenCreateDbCreateTableInsertRow_ThenRowExists;
  end;

implementation

uses
  FireDAC.UI.Intf, FireDAC.Comp.Client, FireDAC.Stan.Intf, FireDAC.Stan.Def,
  FireDAC.Stan.Async, FireDAC.Phys.SQLite, FireDAC.Phys.SQLiteDef, FireDAC.DApt,
  System.Classes, Spring.Collections, Exercise, System.DateUtils, SysUtils;

procedure TestTUsingFireDAC.
  WhenCreateDbCreateTableInsertRow_ThenRowExists;
var
  Connection: TFDConnection;
  ConnectionDefinition: IFDStanConnectionDef;
  ConnectionDefinitionName: string;
  DatabaseFileName: string;
  Rows: IList<TDateTime>;
  Params: TFDPhysSQLiteConnectionDefParams;
  Query: TFDQuery;
  Value: TDateTime;
begin
  FFDGUIxSilentMode := True;
  DatabaseFileName := TPath.Combine(TPath.Combine('..', '..'), TPath.Combine('test-data', 'databasefile.sdb'));
  ConnectionDefinitionName := 'SQLite_Connection';

  DeleteFile(DatabaseFileName);

  ConnectionDefinition := FDManager.ConnectionDefs.AddConnectionDef;
  ConnectionDefinition.Name := ConnectionDefinitionName;
  Params := TFDPhysSQLiteConnectionDefParams(ConnectionDefinition.Params);
  Params.DriverID := 'SQLite';
  Params.Database := DatabaseFileName;
  ConnectionDefinition.Apply;

  Connection := TFDConnection.Create(nil);
  Connection.ConnectionDefName := ConnectionDefinitionName;
  Connection.Connected := True;

  Connection.ExecSQL('CREATE TABLE IF NOT EXISTS exercises (id INTEGER PRIMARY KEY AUTOINCREMENT, start DATETIME)');
  Connection.ExecSQL('INSERT INTO exercises VALUES (NULL, ''2021-10-08T07:00:00.000Z'')');

  Query := TFDQuery.Create(nil);
  Query.Connection := Connection;
  Query.SQL.Text := 'SELECT start FROM exercises';
  Query.Open;

  Rows := TCollections.CreateList<TDateTime>;
  while not Query.Eof do
  begin
    Value := Query.FieldByName('start').AsDateTime;
    Rows.Add(Value);
    Query.Next;
  end;

  Query.Free;
  Connection.Free;

  Assert.AreEqual(1, Rows.Count, 'unexpected number of entries in DB.');
end;

initialization

TDUnitX.RegisterTestFixture(TestTUsingFireDAC);

end.
