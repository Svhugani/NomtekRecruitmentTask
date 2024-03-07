using System;
using UnityEngine;
using Zenject;
using static SceneActor;

public class MainSceneInstallers : MonoInstaller<MainSceneInstallers>
{
    [SerializeField] private AppManager appManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private ActorsManager actorsManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private EnvironmentData environmentData;
    [SerializeField] private GameObject sceneActorPrefab;

    public override void InstallBindings()
    {
        Container.Bind<IAppManager>().FromInstance(appManager).AsSingle();
        Container.Bind<IInputManager>().FromInstance(inputManager).AsSingle();
        Container.Bind<IActorsManager>().FromInstance(actorsManager).AsSingle();
        Container.Bind<IUIManager>().FromInstance(uiManager).AsSingle();
        Container.BindInstance(environmentData).AsSingle();

    }

}
