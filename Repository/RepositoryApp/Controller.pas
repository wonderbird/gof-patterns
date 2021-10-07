unit Controller;

interface

uses
  Repository, Writer, View;

type
  TController = class(TObject)
  private
    FRepository: IRepository;
    FView: TView;
  public
    constructor Create(Repository: IRepository; View: TView); reintroduce;
    procedure ListExercises;

  end;

implementation

uses
  Exercise, System.Generics.Collections;

constructor TController.Create(Repository: IRepository; View: TView);
begin
  inherited Create;
  FRepository := Repository;
  FView := View;
end;

procedure TController.ListExercises;
var
  Records: TList<TExercise>;
begin
  Records := FRepository.Find();
  FView.ShowRecords(Records);
end;

end.
