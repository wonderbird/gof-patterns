unit TestTRepository;

interface

uses
  DUnitX.TestFramework, System.Generics.Collections, Exercise;

type

  [TestFixture]
  TInMemoryRepositoryTests = class
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
  InMemoryRepository;

procedure TInMemoryRepositoryTests.
  Add_ModifyObjectAfterAdd_DoesNotModifyObjectStoredInRepository;
var
  actual: TList<TExercise>;
  rec: TExercise;
  repo: TInMemoryRepository;
begin
  repo := TInMemoryRepository.Create;
  rec.Name := 'Some Exercise';
  repo.Add(rec);

  rec.Name := 'Changed';

  actual := repo.Find();

  Assert.AreEqual('Some Exercise', actual[0].Name);
  repo.Free;
end;

procedure TInMemoryRepositoryTests.Find_AfterAdd_ReturnsListWith1Element;
var
  repo: TInMemoryRepository;
  rec: TExercise;
  actual: TList<TExercise>;
begin
  repo := TInMemoryRepository.Create;
  rec.Name := 'Some Exercise';
  repo.Add(rec);

  actual := repo.Find();

  Assert.AreEqual(1, actual.Count);

  repo.Free;
end;

procedure TInMemoryRepositoryTests.Find_EmptyList_ReturnsListWith0Elements;
var
  repo: TInMemoryRepository;
  actual: TList<TExercise>;
begin
  repo := TInMemoryRepository.Create;

  actual := repo.Find();

  Assert.AreEqual(0, actual.Count);

  repo.Free;
end;

initialization

TDUnitX.RegisterTestFixture(TInMemoryRepositoryTests);

end.
