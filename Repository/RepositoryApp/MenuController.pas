unit MenuController;

interface

uses
  Repository;

type
  TMenuController = class(TObject)
  private
    FRepository: IRepository;
  public
    constructor Create(Repository: IRepository); reintroduce;
  end;

implementation

constructor TMenuController.Create(Repository: IRepository);
begin
  inherited Create;
  FRepository := Repository;
end;

end.
