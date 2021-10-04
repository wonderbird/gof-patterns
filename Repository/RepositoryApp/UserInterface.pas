unit UserInterface;

interface

uses
  Writer, Reader, ConsoleWriter, ConsoleReader;

type
  TUserInterface = class(TObject)
    FReader: IReader;
    FWriter: IWriter;
  public
    constructor Create(Reader: IReader; Writer: IWriter); reintroduce;
    destructor Destroy; override;
    procedure Execute;
  end;

implementation

constructor TUserInterface.Create(Reader: IReader; Writer: IWriter);
begin
  inherited Create;
  FReader := Reader;
  FWriter := Writer;
end;

destructor TUserInterface.Destroy;
begin
  inherited;
end;

procedure TUserInterface.Execute;
var
  choice: string;
begin
  FWriter.Write('Available commands:');
  FWriter.Write('l - List stored records');
  FWriter.Write('q - Quit');
  FWriter.Write('');
  repeat
    FWriter.Write('Your choice:');
    choice := FReader.Read();

    if choice = 'l' then
      FWriter.Write('No records stored.');

  until choice = 'q';
end;

end.
