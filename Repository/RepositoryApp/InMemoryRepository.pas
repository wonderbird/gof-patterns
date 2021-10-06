unit InMemoryRepository;

interface

uses
  Repository, Exercise,
  System.Generics.Collections;

type
  TInMemoryRepository = class(TInterfacedObject, IRepository)
  private
    exercises: TList<TExercise>;
  public
    constructor Create;
    destructor Destroy; override;

    procedure Add(const exercise: TExercise);
    function Find: TList<TExercise>;
  end;

implementation

uses
  Winapi.Windows;

constructor TInMemoryRepository.Create;
begin
  inherited;
  exercises := TList<TExercise>.Create;
end;

destructor TInMemoryRepository.Destroy;
begin
  exercises.Free;
  inherited;
end;

procedure TInMemoryRepository.Add(const exercise: TExercise);
begin
  exercises.Add(exercise);
end;

function TInMemoryRepository.Find: TList<TExercise>;
begin
  Result := exercises;
end;

end.
