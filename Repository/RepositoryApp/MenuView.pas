unit MenuView;

interface

uses
  Writer;

type
  TMenuView = class(TObject)
  private
    FWriter: IWriter;
  public
    constructor Create(Writer: IWriter); reintroduce;
    procedure Print;
  end;

implementation

constructor TMenuView.Create(Writer: IWriter);
begin
  inherited Create;
  FWriter := Writer;
end;

procedure TMenuView.Print;
begin
  FWriter.Write('Available commands:');
  FWriter.Write('a - Add record');
  FWriter.Write('l - List stored records');
  FWriter.Write('q - Quit');
  FWriter.Write('');
end;

end.
