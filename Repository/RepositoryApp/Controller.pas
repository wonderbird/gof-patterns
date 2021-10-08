unit Controller;

interface

uses
  Repository, Writer, View;

type
  IController = interface(IInterface)
    ['{222EEFF7-3895-44F0-8112-BDA28E2354CA}']
    procedure AddExercise(Name: string);
    procedure ListExercises;
  end;

  TController = class(TInterfacedObject, IController)
  private
    FRepository: IRepository;
    FView: IView;
  public
    constructor Create(Repository: IRepository; View: IView); reintroduce;
    procedure AddExercise(Name: string);
    procedure ListExercises;
  end;
implementation

uses
  Exercise, System.Generics.Collections;

constructor TController.Create(Repository: IRepository; View: IView);
begin
  inherited Create;
  FRepository := Repository;
  FView := View;
end;

procedure TController.AddExercise(Name: string);
var
  Exercise: TExercise;
begin
  Exercise.Name := Name;
  FRepository.Add(Exercise);
  FView.ShowMessage('A new record has been added.');
end;

procedure TController.ListExercises;
var
  Records: TList<TExercise>;
begin
  Records := FRepository.Find();
  FView.ShowRecords(Records);
end;

end.
