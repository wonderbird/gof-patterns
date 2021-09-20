unit Repository;

interface

uses
  Exercise,
  System.Generics.Collections;

type
  TRepository = class
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

constructor TRepository.Create;
begin
  exercises := TList<TExercise>.Create;
end;

destructor TRepository.Destroy;
begin
  exercises.Free;
  inherited;
end;

procedure TRepository.Add(const exercise: TExercise);
begin
  exercises.Add(exercise);
end;

function TRepository.Find: TList<TExercise>;
begin
  Result := exercises;
end;

end.
