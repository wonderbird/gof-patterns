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
  Controller: IController;
  Repository: IRepository;
  View: IView;
begin
  Repository := TInMemoryRepository.Create;
  View := TView.Create(FWriter);
  Controller := TController.Create(Repository, View);

  repeat
    View.ShowMenu;
    Choice := FReader.Read();

    if Length(Choice) > 0 then
    begin
      case ord(Choice[1]) of
        ord('l'):
          Controller.ListExercises;
        ord('a'):
          Controller.AddExercise('New Exercise');
      end;
    end;
  until Choice = 'q';
end;

end.
