
public interface IAppManager 
{
    public IInputManager InputManager { get; }
    public IActorsManager ActorsManager { get; }
    public IUIManager UIManager { get; }
    public void InitializeScene();

}
