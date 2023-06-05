unit CsvFileExerciseRepository;

interface

uses
  ExerciseRepository, Exercise, Spring, Spring.Collections, System.IOUtils;

type
  TCsvFileExerciseRepository = class(TInterfacedObject, IExerciseRepository)
  private
    FFilePath: string;
  public
    constructor Create;
    procedure Add(Exercise: TExercise);
    function Find(): IEnumerable<TExercise>; overload;
    function Find(const ThePredicate: Predicate<TExercise>)
      : IEnumerable<TExercise>; overload;
    class function GetDefaultFilePath: string; static;
    procedure SetFilePath(FilePath: string);
    class property DefaultFilePath: string read GetDefaultFilePath;
  end;

implementation

uses
  System.Classes, System.DateUtils, System.SysUtils;

constructor TCsvFileExerciseRepository.Create;
begin
  inherited;
  FFilePath := DefaultFilePath;
end;

procedure TCsvFileExerciseRepository.Add(Exercise: TExercise);
var
  ExistingExercises: IEnumerable<TExercise>;
  ExistingExercise: TExercise;
  Lines: TStringList;
begin
  ExistingExercises := Find;

  Lines := TStringList.Create;
  Lines.Add('Start time and date in UTC');

  for ExistingExercise in ExistingExercises do
    Lines.Add(DateToISO8601(ExistingExercise.Start, True));

  Lines.Add(DateToISO8601(Exercise.Start, True));

  Lines.SaveToFile(FFilePath);
end;

function TCsvFileExerciseRepository.Find(): IEnumerable<TExercise>;
var
  Exercise: TExercise;
  Inspect: TArray<TExercise>;
  Lines: TStringList;
  Line: string;
  LoadedLines: IList<TExercise>;
begin
  Lines := TStringList.Create;

  if FileExists(FFilePath) then
  begin
    Lines.LoadFromFile(FFilePath);
    Lines.Delete(0);
  end;

  LoadedLines := TCollections.CreateList<TExercise>;
  for Line in Lines do
  begin
    Exercise.Start := ISO8601ToDate(Line, True);
    LoadedLines.Add(Exercise);
  end;

  Result := LoadedLines;
end;

function TCsvFileExerciseRepository.Find(const ThePredicate
  : Predicate<TExercise>): IEnumerable<TExercise>;
var
  AllExercises: IEnumerable<TExercise>;
begin
  AllExercises := Find;
  Result := AllExercises.Where(ThePredicate);
end;

class function TCsvFileExerciseRepository.GetDefaultFilePath: string;
begin
  Result := TPath.Combine(TPath.Combine('..', '..'), TPath.Combine('test-data',
    'default.csv'));
end;

procedure TCsvFileExerciseRepository.SetFilePath(FilePath: string);
begin
  FFilePath := FilePath;
end;

end.
