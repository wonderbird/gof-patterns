unit UserInterface;

interface

uses
  Writer, Reader, ConsoleWriter, ConsoleReader;

type
  TUserInterface = class(TObject)
  var
     writer: IWriter;
     reader: IReader;
  public
    constructor Create(_writer: IWriter; _reader: IReader); overload;
    constructor Create; overload;
    destructor Destroy; override;
    procedure Execute;
  end;

implementation

constructor TUserInterface.Create(_writer: IWriter; _reader: IReader);
begin
  writer := _writer;
  reader := _reader;
end;

constructor TUserInterface.Create;
begin
  inherited;
  writer := TConsoleWriter.Create;
  reader := TConsoleReader.Create;
end;

destructor TUserInterface.Destroy;
begin
  inherited;
end;

procedure TUserInterface.Execute;
begin
  writer.Write('Hello World!');
  reader.Read;
end;

end.
