unit Repository;

interface

uses
  Exercise, System.Generics.Collections;

type
  IRepository = interface(IInterface)
    ['{7B6740C9-4726-449B-BD3F-1DB56335B774}']
    procedure Add(const Exercise: TExercise);
    function Find: TList<TExercise>;
  end;

implementation

end.
