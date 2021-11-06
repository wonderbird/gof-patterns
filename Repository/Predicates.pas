unit Predicates;

interface

uses
  Exercise, Spring;

type
  TPredicates = class
  public
    class function IsInRange(LowerDateTime: TDateTime; UpperDateTime: TDateTime)
      : Predicate<TExercise>; static;
  end;

implementation

class function TPredicates.IsInRange(LowerDateTime: TDateTime;
  UpperDateTime: TDateTime): Predicate<TExercise>;
begin
  Result := function(const Exercise: TExercise): Boolean
    begin
      Result := (LowerDateTime <= Exercise.Start) and
        (Exercise.Start <= UpperDateTime);
    end;
end;

end.
