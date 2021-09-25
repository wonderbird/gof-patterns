unit ConsoleWriter;

interface

uses
  Writer;

type
  TConsoleWriter = class(TInterfacedObject, IWriter)
  public
    procedure Write(msg: string);
  end;

implementation

procedure TConsoleWriter.Write(msg: string);
begin
  Writeln(msg);
end;

end.
