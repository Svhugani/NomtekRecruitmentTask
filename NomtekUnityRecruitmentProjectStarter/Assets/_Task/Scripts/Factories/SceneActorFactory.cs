using UnityEngine;
using Zenject;

public class SceneActorFactory : IFactory<GameObject, SceneActor>
{
    private DiContainer _container;
    public SceneActorFactory(DiContainer container)
    {
        _container = container;
    }

    public SceneActor Create(GameObject actorPrefab)
    {
        SceneActor actor = _container.InstantiatePrefabForComponent<SceneActor>(actorPrefab);
        _container.Inject(actor);
        return actor;
    }
}