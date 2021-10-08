unit View;

interface

uses
  Writer, System.Generics.Collections, Exercise;

type
  IView = interface(IInterface)
    ['{99521C8E-33C1-474A-BEF0-26CFB9B3B723}']
    procedure Present;
    procedure ShowMessage(Msg: string);
    procedure ShowRecords(Records: TList<TExercise>);
  end;

  TView = class(TInterfacedObject, IView)
  private
    FWriter: IWriter;
  public
    constructor Create(Writer: IWriter); reintroduce;
    procedure Present;
    procedure ShowMessage(Msg: string);
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

procedure TView.Present;
begin
  FWriter.Write('');
  FWriter.Write('Available commands:');
  FWriter.Write('a - Add record');
  FWriter.Write('l - List stored records');
  FWriter.Write('q - Quit');
  FWriter.Write('');
  FWriter.Write('Your choice:');
end;

procedure TView.ShowMessage(Msg: string);
begin
  FWriter.Write(Msg);
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
