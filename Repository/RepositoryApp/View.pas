unit View;

interface

uses
  Writer, System.Generics.Collections, Exercise;

type
  TView = class(TObject)
  private
    FWriter: IWriter;
  public
    constructor Create(Writer: IWriter); reintroduce;
    procedure ShowMenu;
    procedure ShowRecords(Records: TList<TExercise>);
  end;

implementation

uses
  SysUtils;

constructor TView.Create(Writer: IWriter);
begin
  inherited Create;
  FWriter := Writer;
end;

procedure TView.ShowMenu;
begin
  FWriter.Write('');
  FWriter.Write('Available commands:');
  FWriter.Write('a - Add record');
  FWriter.Write('l - List stored records');
  FWriter.Write('q - Quit');
  FWriter.Write('');
  FWriter.Write('Your choice:');
end;

procedure TView.ShowRecords(Records: TList<TExercise>);
var
  Exercise: TExercise;
  Index: Integer;
begin
  if Records.Count = 0 then
    FWriter.Write('No records stored.')
  else
    for Index := 1 to Records.Count do
    begin
      Exercise := Records[Index - 1];
      FWriter.Write(Format('%d: %s', [Index, Exercise.Name]));
    end;
end;

end.
