unit SqliteDatabaseConfiguration;

interface

type
  TSqliteDatabaseConfiguration = class
  const
    ConnectionDefinitionName = 'SQLite_Connection';
  private
    class procedure CreateTemporaryConnectionDefinition; static;
    class function GetDatabaseFileName: string; static;
  public
    class procedure InitializeFireDAC; static;
    class property DatabaseFileName: string read GetDatabaseFileName;
  end;

implementation

uses
  System.IOUtils,
  FireDAC.UI.Intf, FireDAC.Stan.Intf, FireDAC.Phys.SQLiteDef, FireDAC.Comp.Client,

  // The following FireDAC units provide functionality at runtime.
  // Exceptions are caused if the units are missing.
  FireDAC.Stan.Def, FireDAC.Phys.SQLite, FireDAC.Stan.Async, FireDAC.DApt;

class procedure TSqliteDatabaseConfiguration.CreateTemporaryConnectionDefinition;
var
  ConnectionDefinition: IFDStanConnectionDef;
  Params: TFDPhysSQLiteConnectionDefParams;
begin
  ConnectionDefinition := FDManager.ConnectionDefs.AddConnectionDef;
  ConnectionDefinition.Name := ConnectionDefinitionName;
  Params := TFDPhysSQLiteConnectionDefParams(ConnectionDefinition.Params);
  Params.DriverID := 'SQLite';
  Params.Database := DatabaseFileName;
  ConnectionDefinition.Apply;
end;

class function TSqliteDatabaseConfiguration.GetDatabaseFileName: string;
begin
  Result := TPath.Combine(TPath.Combine('..', '..'), TPath.Combine('test-data', 'databasefile.sdb'));
end;

class procedure TSqliteDatabaseConfiguration.InitializeFireDAC;
begin
  FFDGUIxSilentMode := True;
  CreateTemporaryConnectionDefinition;
end;

initialization

TSqliteDatabaseConfiguration.InitializeFireDAC;

end.
