unit ConsoleReader;

interface

uses
  Reader;

type
  TConsoleReader = class(TInterfacedObject, IReader)
  public
    function Read: string;
  end;

implementation

function TConsoleReader.Read: string;
var
  input: string;
begin
  Readln(input);
  Result := input;
end;

end.
