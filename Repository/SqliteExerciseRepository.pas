unit SqliteExerciseRepository;

interface

uses
  ExerciseRepository, Exercise, Spring, Spring.Collections;

type
  TSqliteExerciseRepository = class(TInterfacedObject, IExerciseRepository)
  public
    procedure Add(Exercise: TExercise);
    function Find(): IEnumerable<TExercise>; overload;
    function Find(const ThePredicate: Predicate<TExercise>):
        IEnumerable<TExercise>; overload;
  end;

implementation

procedure TSqliteExerciseRepository.Add(Exercise: TExercise);
begin
  // TODO -cMM: TSqliteExerciseRepository.Add default body inserted
end;

function TSqliteExerciseRepository.Find: IEnumerable<TExercise>;
begin
  Result := nil;
  // TODO -cMM: TSqliteExerciseRepository.Find default body inserted
end;

function TSqliteExerciseRepository.Find(const ThePredicate:
    Predicate<TExercise>): IEnumerable<TExercise>;
begin
  Result := nil;
  // TODO -cMM: TSqliteExerciseRepository.Find default body inserted
end;

end.
