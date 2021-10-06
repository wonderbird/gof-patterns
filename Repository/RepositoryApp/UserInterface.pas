unit UserInterface;

interface

uses
  Writer, Reader, ConsoleWriter, ConsoleReader;

type
  TUserInterface = class(TObject)
  private
    FReader: IReader;
    FWriter: IWriter;
  public
    constructor Create(Reader: IReader; Writer: IWriter); reintroduce;
    destructor Destroy; override;
    procedure Execute;
  end;

implementation

uses
  Repository, InMemoryRepository, Exercise, System.Generics.Collections, System.SysUtils, MenuView,
  MenuController;

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
  Choice: string;
  Controller: TMenuController;
  Exercise: TExercise;
  Records: TList<TExercise>;
  Repository: IRepository;
  Index: Integer;
  Menu: TMenuView;
begin
  Repository := TInMemoryRepository.Create;
  Controller := TMenuController.Create(Repository);
  Menu := TMenuView.Create(FWriter);

  Menu.Print;
  repeat
    FWriter.Write('Your choice:');
    Choice := FReader.Read();

    if Choice = 'l' then
    begin
      Records := Repository.Find();
      if Records.Count = 0 then
        FWriter.Write('No records stored.')
      else
        for Index := 1 to Records.Count do
        begin
          Exercise := Records[Index - 1];
          FWriter.Write(Format('%d: %s', [Index, Exercise.Name]));
        end;
    end
    else if Choice = 'a' then
    begin
      Exercise.Name := 'New Exercise';
      Repository.Add(Exercise);
      FWriter.Write('A new record has been added.');
    end;

  until Choice = 'q';

  Menu.Free;
end;

end.
