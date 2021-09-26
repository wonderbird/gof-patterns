unit TestTUserInterface;

interface

uses
  DUnitX.TestFramework, Delphi.Mocks, UserInterface, Writer, Reader,
  System.Classes;

type

  [TestFixture]
  TUserInterfaceTest = class
  private
    mockWriter: TMock<IWriter>;
    stubReader: TStub<IReader>;
    procedure SetupWriterExpectations(const expectedMessages: TStringList);
  public
    [Test]
    procedure Execute_InputIsEmpty_RepeatsInputPrompt;
    [Test]
    procedure Execute_PrintsUsageInstructions;
    [Setup]
    procedure Setup;
    [TearDown]
    procedure TearDown;
  end;

implementation

uses
  Rtti;

procedure TUserInterfaceTest.Execute_InputIsEmpty_RepeatsInputPrompt;
var
  userInterface: TUserInterface;
  MyClass: TComponent;
begin
  mockWriter.Setup.Expect.Exactly(2).When.Write('Your choice:');
  stubReader.Setup.WillReturn('').When.Read;

  userInterface := TUserInterface.Create(mockWriter.Instance, stubReader.Instance);

  try
    userInterface.Execute;

    mockWriter.Verify;
    Assert.Pass;
  finally
    userInterface.Free;
  end;
end;

procedure TUserInterfaceTest.Execute_PrintsUsageInstructions;
var
  userInterface: TUserInterface;
  expectedMessages: TStringList;
  index: Integer;
begin
  expectedMessages := TStringList.Create;
  expectedMessages.Add('Available commands:');
  expectedMessages.Add('q - Quit');

  SetupWriterExpectations(expectedMessages);
  stubReader.Setup.WillReturn('q').When.Read;

  userInterface := TUserInterface.Create(mockWriter.Instance,
    stubReader.Instance);

  try
    userInterface.Execute;

    mockWriter.Verify();
    Assert.Pass();
  finally
    userInterface.Free;
  end;

end;

procedure TUserInterfaceTest.Setup;
begin
  mockWriter := TMock<IWriter>.Create;
  stubReader := TStub<IReader>.Create;
end;

procedure TUserInterfaceTest.SetupWriterExpectations(const expectedMessages
  : TStringList);
var
  index: Integer;
begin
  for index := 0 to expectedMessages.Count - 1 do
  begin
    mockWriter.Setup.Expect.Once.When.Write(expectedMessages[index]);
  end;
end;

procedure TUserInterfaceTest.TearDown;
begin
  mockWriter.Free;
  stubReader.Free;
end;

initialization

TDUnitX.RegisterTestFixture(TUserInterfaceTest);

end.
