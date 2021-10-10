unit ExerciseRepository;

interface

uses
  Spring, Spring.Collections, Exercise;

type
  IExerciseRepository = interface(IInterface)
    ['{6281E974-ED0A-4058-8B8E-59168F2B3538}']
    procedure Add(Exercise: TExercise);
    function Find(): IEnumerable<TExercise>; overload;
    function Find(const ThePredicate: Predicate<TExercise>): IEnumerable<TExercise>; overload;
  end;

implementation

end.
