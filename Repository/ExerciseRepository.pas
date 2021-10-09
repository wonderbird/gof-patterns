unit ExerciseRepository;

interface

uses
  System.Generics.Collections, Exercise;

type
  IExerciseRepository = interface(IInterface)
    ['{6281E974-ED0A-4058-8B8E-59168F2B3538}']
    procedure Add(Exercise: TExercise);
    function Find: TList<TExercise>;
  end;

implementation

end.
