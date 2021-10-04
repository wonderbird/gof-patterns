program RepositoryApp;

{$APPTYPE CONSOLE}
{$R *.res}

uses
  System.SysUtils,
  Repository in 'Repository.pas',
  Exercise in 'Exercise.pas',
  UserInterface in 'UserInterface.pas',
  ConsoleWriter in 'ConsoleWriter.pas',
  Writer in 'Writer.pas',
  Reader in 'Reader.pas',
  ConsoleReader in 'ConsoleReader.pas';

var
  input: string;
  reader: IReader;
  writer: IWriter;
  UserInterface: TUserInterface;

begin
  try
    reader := TConsoleReader.Create;
    writer := TConsoleWriter.Create;
    UserInterface := TUserInterface.Create(reader, writer);
    UserInterface.Execute;
    UserInterface.Free;
  except
    on E: Exception do
      Writeln(E.ClassName, ': ', E.Message);
  end;

end.
