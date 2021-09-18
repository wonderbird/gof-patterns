unit Repository;

interface

uses
  Exercise,
  System.Generics.Collections;

type
  TRepository = class
  public
    procedure Add(Exercise: TExercise);
    function Find: TList<TExercise>;
  end;

implementation

uses
  Winapi.Windows;

procedure TRepository.Add(Exercise: TExercise);
begin

end;

function TRepository.Find: TList<TExercise>;
begin
  Result := TList<TExercise>.Create;
end;

end.
