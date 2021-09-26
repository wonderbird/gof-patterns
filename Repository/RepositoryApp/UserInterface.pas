unit UserInterface;

interface

uses
  Writer, Reader, ConsoleWriter, ConsoleReader;

type
  TUserInterface = class(TObject)
    Reader: IReader;
    Writer: IWriter;
  public
    constructor Create; overload;
    constructor Create(_writer: IWriter; _reader: IReader); overload;
    destructor Destroy; override;
    procedure Execute;
  end;

implementation

constructor TUserInterface.Create;
begin
  inherited;
  Writer := TConsoleWriter.Create;
  Reader := TConsoleReader.Create;
end;

constructor TUserInterface.Create(_writer: IWriter; _reader: IReader);
begin
  Writer := _writer;
  Reader := _reader;
end;

destructor TUserInterface.Destroy;
begin
  inherited;
end;

procedure TUserInterface.Execute;
var
  choice: string;
begin
  Writer.Write('Available commands:');
  Writer.Write('q - Quit');
  Writer.Write('');
  repeat
    Writer.Write('Your choice:');
    choice := Reader.Read();
  until choice = 'q';
end;

end.
