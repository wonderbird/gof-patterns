unit TestTUserInterface;

interface

uses
  DUnitX.TestFramework, Spring.Mocking, UserInterface, Writer, Reader,
  System.Classes;

type

  [TestFixture]
  TUserInterfaceTest = class
  private
    FMockWriter: Mock<IWriter>;
    FStubReader: Mock<IReader>;
  public
    [Test]
    procedure Execute_InputIsEmpty_RepeatsInputPrompt;
    [Test]
    procedure Execute_NoRecords_AddRecordListRecords_PrintsOneRecord;
    [Test]
    procedure Execute_NoRecords_ListRecords_PrintsEmptyList;
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
  FStubReader.Setup.Returns<string>(['', 'q']).When.Read;

  UserInterface := TUserInterface.Create(FStubReader.Instance,
    FMockWriter.Instance);

  try
    UserInterface.Execute;

    FMockWriter.Received(Times.Exactly(2)).Write('Your choice:');
    Assert.Pass;
  finally
    UserInterface.Free;
  end;
end;

procedure
    TUserInterfaceTest.Execute_NoRecords_AddRecordListRecords_PrintsOneRecord;
var
  UserInterface: TUserInterface;
begin
  FStubReader.Setup.Returns(['a', 'l', 'q']).When.Read;

  UserInterface := TUserInterface.Create(FStubReader, FMockWriter);
  try
    UserInterface.Execute;

    FMockWriter.Received.Write('A new record has been added.');
    FMockWriter.Received.Write('1: New Exercise');
    Assert.Pass();
  finally
    UserInterface.Free;
  end;
end;

procedure TUserInterfaceTest.Execute_NoRecords_ListRecords_PrintsEmptyList;
var
  UserInterface: TUserInterface;
begin
  FStubReader.Setup.Returns(['l', 'q']).When.Read;

  UserInterface := TUserInterface.Create(FStubReader.Instance, FMockWriter.Instance);

  try
    UserInterface.Execute;

    FMockWriter.Received.Write('No records stored.');
    Assert.Pass();
  finally
    UserInterface.Free;
  end;

end;

procedure TUserInterfaceTest.Execute_PrintsUsageInstructions;
var
  UserInterface: TUserInterface;
  index: Integer;
begin
  FStubReader.Setup.Returns('q').When.Read;

  UserInterface := TUserInterface.Create(FStubReader.Instance,
    FMockWriter.Instance);

  try
    UserInterface.Execute;

    FMockWriter.Received(Times.Once).Write('Available commands:');
    FMockWriter.Received(Times.Once).Write('a - Add record');
    FMockWriter.Received(Times.Once).Write('l - List stored records');
    FMockWriter.Received(Times.Once).Write('q - Quit');
    Assert.Pass();
  finally
    UserInterface.Free;
  end;

end;

procedure TUserInterfaceTest.Setup;
begin
  FMockWriter.Reset;
end;

initialization

TDUnitX.RegisterTestFixture(TUserInterfaceTest);

end.
