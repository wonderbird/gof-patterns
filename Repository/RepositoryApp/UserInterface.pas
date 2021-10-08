unit UserInterface;

interface

uses
  Writer, Reader, ConsoleWriter, ConsoleReader;

type
  TCommand = (AddExercise, ListExercises, Quit, None);

type
  TUserInterface = class(TObject)
  private
    FReader: IReader;
    FWriter: IWriter;
  public
    constructor Create(Reader: IReader; Writer: IWriter); reintroduce;
    destructor Destroy; override;
    procedure Execute;
    function ReceiveAndProcessCommand: TCommand;
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
  Command: TCommand;
  Controller: IController;
  Repository: IRepository;
  View: IView;
begin
  Repository := TInMemoryRepository.Create;
  View := TView.Create(FWriter);
  Controller := TController.Create(Repository, View);

  repeat
    View.ShowMenu;
    Command := ReceiveAndProcessCommand;

    case Command of
      ListExercises:
        Controller.ListExercises;
      AddExercise:
        Controller.AddExercise('New Exercise');
    end;
  until Command = Quit;
end;

function TUserInterface.ReceiveAndProcessCommand: TCommand;
var
  Choice: string;
  CommandMap: TDictionary<string, TCommand>;
begin
  Choice := FReader.Read();
  CommandMap := TDictionary<string, TCommand>.Create;
  CommandMap.Add('', None);
  CommandMap.Add('l', ListExercises);
  CommandMap.Add('a', AddExercise);
  CommandMap.Add('q', Quit);
  Result := CommandMap[Choice];
end;

end.
