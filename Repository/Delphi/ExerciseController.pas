unit ExerciseController;

interface

uses
  Exercise,
  Spring.Collections, ExerciseRepository;

type
  TExerciseController = class(TObject)
  private
    FRepository: IExerciseRepository;
  public
    constructor Create(Repository: IExerciseRepository); reintroduce;
    procedure AddExercise(Exercise: TExercise);
    function FindExercisesStartedInTimePeriod(LowerDateTime, UpperDateTime:
        TDateTime): IEnumerable<TExercise>;
    function ListExercises: IEnumerable<TExercise>;
  end;

implementation

constructor TExerciseController.Create(Repository: IExerciseRepository);
begin
  inherited Create;
  FRepository := Repository;
end;

procedure TExerciseController.AddExercise(Exercise: TExercise);
begin
  FRepository.Add(Exercise);
end;

function TExerciseController.FindExercisesStartedInTimePeriod(LowerDateTime,
    UpperDateTime: TDateTime): IEnumerable<TExercise>;
begin
  Result := FRepository.Find(function (const Exercise: TExercise): Boolean
  begin
    Result := (LowerDateTime <= Exercise.Start) and (Exercise.Start <= UpperDateTime);
  end);
end;

function TExerciseController.ListExercises: IEnumerable<TExercise>;
begin
  Result := FRepository.Find;
end;

end.
