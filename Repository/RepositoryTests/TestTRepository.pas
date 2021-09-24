unit TestTRepository;

interface

uses
  DUnitX.TestFramework, System.Generics.Collections, Exercise;

type

  [TestFixture]
  TRepositoryTests = class
  public
    [Test]
    procedure Add_ModifyObjectAfterAdd_DoesNotModifyObjectStoredInRepository;
    [Test]
    procedure Find_AfterAdd_ReturnsListWith1Element;
    [Test]
    procedure Find_EmptyList_ReturnsListWith0Elements;
  end;

implementation

uses
  Repository;

procedure TRepositoryTests.
  Add_ModifyObjectAfterAdd_DoesNotModifyObjectStoredInRepository;
var
  actual: TList<TExercise>;
  rec: TExercise;
  repo: TRepository;
begin
  repo := TRepository.Create;
  rec.Name := 'Some Exercise';
  repo.Add(rec);

  rec.Name := 'Changed';

  actual := repo.Find();

  Assert.AreEqual('Some Exercise', actual[0].Name);
  repo.Free;
end;

procedure TRepositoryTests.Find_AfterAdd_ReturnsListWith1Element;
var
  repo: TRepository;
  rec: TExercise;
  actual: TList<TExercise>;
begin
  repo := TRepository.Create;
  rec.Name := 'Some Exercise';
  repo.Add(rec);

  actual := repo.Find();

  Assert.AreEqual(1, actual.Count);

  repo.Free;
end;

procedure TRepositoryTests.Find_EmptyList_ReturnsListWith0Elements;
var
  repo: TRepository;
  actual: TList<TExercise>;
begin
  repo := TRepository.Create;

  actual := repo.Find();

  Assert.AreEqual(0, actual.Count);

  repo.Free;
end;

initialization

TDUnitX.RegisterTestFixture(TRepositoryTests);

end.
