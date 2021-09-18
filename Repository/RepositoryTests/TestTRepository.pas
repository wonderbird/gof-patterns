unit TestTRepository;

interface

uses
  DUnitX.TestFramework;

type
  [TestFixture]
  TRepositoryTests = class
  public
    [Setup]
    procedure Setup;
    [TearDown]
    procedure TearDown;
    // Sample Methods
    // Simple single Test
    [Test]
    procedure Test1;
    // Test with TestCase Attribute to supply parameters.
    [Test]
    [TestCase('TestA','1,2')]
    [TestCase('TestB','3,4')]
    procedure Test2(const AValue1 : Integer;const AValue2 : Integer);
  end;

implementation

procedure TRepositoryTests.Setup;
begin
end;

procedure TRepositoryTests.TearDown;
begin
end;

procedure TRepositoryTests.Test1;
begin
end;

procedure TRepositoryTests.Test2(const AValue1 : Integer;const AValue2 : Integer);
begin
end;

initialization
  TDUnitX.RegisterTestFixture(TRepositoryTests);

end.
