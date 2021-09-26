unit TestTUserInterface;

interface

uses
  DUnitX.TestFramework, Spring.Mocking, UserInterface, Writer, Reader,
  System.Classes;

type

  [TestFixture]
  TUserInterfaceTest = class
  private
    mockWriter: Mock<IWriter>;
    stubReader: Mock<IReader>;
  public
    [Test]
    procedure Execute_InputIsEmpty_RepeatsInputPrompt;
    [Test]
    procedure Execute_PrintsUsageInstructions;
    [Setup]
    procedure Setup;
  end;

implementation

uses
  Rtti;

procedure TUserInterfaceTest.Execute_InputIsEmpty_RepeatsInputPrompt;
var
  UserInterface: TUserInterface;
  MyClass: TComponent;
begin
  stubReader.Setup.Returns<string>(['', 'q']).When.Read;

  UserInterface := TUserInterface.Create(mockWriter.Instance,
    stubReader.Instance);

  try
    UserInterface.Execute;

    mockWriter.Received(Times.Exactly(2)).Write('Your choice:');
    Assert.Pass;
  finally
    UserInterface.Free;
  end;
end;

procedure TUserInterfaceTest.Execute_PrintsUsageInstructions;
var
  UserInterface: TUserInterface;
  index: Integer;
begin
  stubReader.Setup.Returns('q').When.Read;

  UserInterface := TUserInterface.Create(mockWriter.Instance,
    stubReader.Instance);

  try
    UserInterface.Execute;

    mockWriter.Received(Times.Once).Write('Available commands:');
    mockWriter.Received(Times.Once).Write('q - Quit');
    Assert.Pass();
  finally
    UserInterface.Free;
  end;

end;

procedure TUserInterfaceTest.Setup;
begin
  mockWriter.Reset;
end;

initialization

TDUnitX.RegisterTestFixture(TUserInterfaceTest);

end.
