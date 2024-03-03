using Zenject;

public class MainSceneInstallers : MonoInstaller<MainSceneInstallers>
{
    public override void InstallBindings()
    {
        var appManager = FindObjectOfType<AppManager>();
        Container.Bind<IAppManager>().FromInstance(appManager).AsSingle();

        var inputManager = FindObjectOfType<InputManager>();
        Container.Bind<IInputManager>().FromInstance(inputManager).AsSingle();

        var actorsManager = FindObjectOfType<ActorsManager>();
        Container.Bind<IActorsManager>().FromInstance(actorsManager).AsSingle();

        var uiManager = FindObjectOfType<UIManager>();
        Container.Bind<IUIManager>().FromInstance(uiManager).AsSingle();
    }
}
