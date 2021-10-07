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
  Repository, InMemoryRepository, Exercise, System.Generics.Collections, View,
  Controller;

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
  Controller: TController;
  Exercise: TExercise;
  Records: TList<TExercise>;
  Repository: IRepository;
  Index: Integer;
  View: TView;
begin
  Repository := TInMemoryRepository.Create;
  View := TView.Create(FWriter);
  Controller := TController.Create(Repository, View);

  repeat
    View.ShowMenu;
    Choice := FReader.Read();

    if Choice = 'l' then
    begin
      Controller.ListExercises;
    end
    else if Choice = 'a' then
    begin
      Exercise.Name := 'New Exercise';
      Repository.Add(Exercise);
      FWriter.Write('A new record has been added.');
    end;

  until Choice = 'q';

  View.Free;
end;

end.
