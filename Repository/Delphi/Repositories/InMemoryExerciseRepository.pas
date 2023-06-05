unit InMemoryExerciseRepository;

interface

uses
  Spring, Spring.Collections, Exercise, ExerciseRepository;

type
  TInMemoryExerciseRepository = class(TInterfacedObject, IExerciseRepository)
  private
    FExercises: IList<TExercise>;
  public
    constructor Create;
    procedure Add(Exercise: TExercise);
    function Find(): IEnumerable<TExercise>; overload;
    function Find(const ThePredicate: Predicate<TExercise>): IEnumerable<TExercise>; overload;
  end;

implementation

constructor TInMemoryExerciseRepository.Create;
begin
  inherited;
  FExercises := TCollections.CreateList<TExercise>;
end;

procedure TInMemoryExerciseRepository.Add(Exercise: TExercise);
begin
  FExercises.Add(Exercise);
end;

function TInMemoryExerciseRepository.Find: IEnumerable<TExercise>;
begin
  Result := FExercises;
end;

function TInMemoryExerciseRepository.Find(const ThePredicate: Predicate<TExercise>): IEnumerable<TExercise>;
begin
  Result := FExercises.Where(ThePredicate);
end;

end.
