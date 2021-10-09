unit ExerciseController;

interface

uses
  Exercise,
  System.Generics.Collections, ExerciseRepository;

type
  TExerciseController = class(TObject)
  private
    FRepository: IExerciseRepository;
  public
    constructor Create(Repository: IExerciseRepository); reintroduce;
    function ListExercises: TList<TExercise>;
  end;

implementation

constructor TExerciseController.Create(Repository: IExerciseRepository);
begin
  inherited Create;
  FRepository := Repository;
end;

function TExerciseController.ListExercises: TList<TExercise>;
begin
  Result := FRepository.Find;
end;

end.
