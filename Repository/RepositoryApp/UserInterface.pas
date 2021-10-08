unit UserInterface;

interface

uses
  Writer, Reader, ConsoleWriter, ConsoleReader, QuitContinue;

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

  IMenuController = interface(IInterface)
    ['{EF9F1A32-5303-46A1-A3FE-22C3EA7A1F81}']
    function ReceiveAndProcessUserAction: TQuitContinue;
  end;

  TMenuController = class(TInterfacedObject, IMenuController)

  public
    function ReceiveAndProcessUserAction: TQuitContinue;
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
  Controller : IMenuController;
  OtherController: IController;
  Repository: IRepository;
  View: IView;
begin
  Repository := TInMemoryRepository.Create;
  View := TView.Create(FWriter);
  OtherController := TController.Create(Repository, View);
  Controller := TMenuController.Create;

  repeat
    View.Present;
    // TODO: continue extractin a menu controller and separating the menu actions from the commands resulting from menu actions
    Controller.ReceiveAndProcessUserAction;
    Command := ReceiveAndProcessCommand;

    case Command of
      ListExercises:
        OtherController.ListExercises;
      AddExercise:
        OtherController.AddExercise('New Exercise');
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

function TMenuController.ReceiveAndProcessUserAction: TQuitContinue;
begin
  Result := Continue;
end;

end.
