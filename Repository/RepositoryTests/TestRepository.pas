unit TestRepository;

interface

uses
  DUnitX.TestFramework;

type

  [TestFixture]
  TestTRepository = class(TTestCase)
  published
    procedure TestFind;
  end;

implementation

procedure TestTRepository.TestFind;
begin
  CheckEquals(12, 12);
end;

initialization

TDUnitX.RegisterTestFixture(TestTRepository);

end.
