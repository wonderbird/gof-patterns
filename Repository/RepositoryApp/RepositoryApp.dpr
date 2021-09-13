program RepositoryApp;

{$APPTYPE CONSOLE}
{$R *.res}

uses
  System.SysUtils;

var
  input: string;

begin
  try
    Writeln('Hello World!');
    Readln(input);
  except
    on E: Exception do
      Writeln(E.ClassName, ': ', E.Message);
  end;

end.
