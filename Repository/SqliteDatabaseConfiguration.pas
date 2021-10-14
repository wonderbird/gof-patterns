unit SqliteDatabaseConfiguration;

interface

type
  TSqliteDatabaseConfiguration = class
  const
    ConnectionDefinitionName = 'SQLite_Connection';
  private
    class function GetDatabaseFileName: string; static;
  public
    class procedure InitializeFireDAC;
    class property DatabaseFileName: string read GetDatabaseFileName;

  end;

implementation

uses
  System.IOUtils,
  FireDAC.UI.Intf;

class function TSqliteDatabaseConfiguration.GetDatabaseFileName: string;
begin
  Result := TPath.Combine(TPath.Combine('..', '..'), TPath.Combine('test-data', 'databasefile.sdb'));
end;

class procedure TSqliteDatabaseConfiguration.InitializeFireDAC;
begin
  FFDGUIxSilentMode := True;
end;

initialization

TSqliteDatabaseConfiguration.InitializeFireDAC;

end.
