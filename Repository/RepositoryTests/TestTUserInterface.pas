unit TestTUserInterface;

interface

uses
  DUnitX.TestFramework, Delphi.Mocks, UserInterface, Writer, Reader;

type

  [TestFixture]
  TUserInterfaceTest = class
  public
    [Test]
    procedure Execute_PrintsUsageInstructions;
  end;

implementation

uses
  Rtti;

{ TUserInterfaceTest }

procedure TUserInterfaceTest.Execute_PrintsUsageInstructions;
var
  mockReader: TMock<IReader>;
  mockWriter: TMock<IWriter>;
  UserInterface: TUserInterface;
begin
  mockWriter := TMock<IWriter>.Create;
  mockWriter.Setup.Expect.Once.When.Write(It(0).IsAny<string>());

  mockReader := TMock<IReader>.Create;
  mockReader.Setup.WillReturn('').When.Read;
  mockReader.Setup.Expect.Once.When.Read;

  UserInterface := TUserInterface.Create(mockWriter.Instance,
    mockReader.Instance);
  UserInterface.Execute;
  UserInterface.Free;

  mockReader.Verify();
  mockWriter.Verify();
  Assert.Pass();
end;

initialization

TDUnitX.RegisterTestFixture(TUserInterfaceTest);

end.
