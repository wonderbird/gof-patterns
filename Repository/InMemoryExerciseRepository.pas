unit InMemoryExerciseRepository;

interface

uses
  Exercise, ExerciseRepository, System.Generics.Collections;

type
  TInMemoryExerciseRepository = class(TInterfacedObject, IExerciseRepository)
  private
    FExercises: TList<TExercise>;
  public
    constructor Create;
    destructor Destroy; override;
    procedure Add(Exercise: TExercise);
    function Find: TList<TExercise>;
  end;

implementation

constructor TInMemoryExerciseRepository.Create;
begin
  inherited;
  FExercises := TList<TExercise>.Create;
end;

destructor TInMemoryExerciseRepository.Destroy;
begin
  FExercises.Free;
  inherited;
end;

procedure TInMemoryExerciseRepository.Add(Exercise: TExercise);
begin
  FExercises.Add(Exercise);
end;

function TInMemoryExerciseRepository.Find: TList<TExercise>;
begin
  Result := FExercises;
end;

end.
