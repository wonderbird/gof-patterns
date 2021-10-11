unit CsvFileExerciseRepository;

interface

uses
  ExerciseRepository, Exercise, Spring, Spring.Collections;

type
  TCsvFileExerciseRepository = class(TInterfacedObject, IExerciseRepository)


  public
    constructor Create;
    procedure Add(Exercise: TExercise);
    function Find(): IEnumerable<TExercise>; overload;
    function Find(const ThePredicate: Predicate<TExercise>): IEnumerable<TExercise>; overload;
  end;

implementation

constructor TCsvFileExerciseRepository.Create;
begin
  inherited;
end;

procedure TCsvFileExerciseRepository.Add(Exercise: TExercise);
begin
end;

function TCsvFileExerciseRepository.Find(): IEnumerable<TExercise>;
begin
  Result := nil;
end;

function TCsvFileExerciseRepository.Find(const ThePredicate: Predicate<TExercise>):
    IEnumerable<TExercise>;
begin
  Result := nil;
end;

end.
