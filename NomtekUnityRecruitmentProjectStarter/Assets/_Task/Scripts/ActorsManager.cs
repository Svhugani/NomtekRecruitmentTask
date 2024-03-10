using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ActorsManager : MonoBehaviour, IActorsManager
{
    [SerializeField] private GameObject ground;
    [SerializeField] private LayerMask placementLayerMask;

    private EnvironmentData _environmentData;
    private SceneActor _previewActor;
    private readonly Vector3 _previewInitPosition = new Vector3(100, 0, 0);

    private SceneActor.Factory _sceneActorFactory;

    [Inject]
    public void Construct(EnvironmentData environmentData, SceneActor.Factory sceneActorFactory)
    {
        _environmentData = environmentData;
        _sceneActorFactory = sceneActorFactory;
        _environmentData.OnActorRegistered += delegate { Debug.Log($"Actors: {_environmentData.SceneActors.Count}"); };
        _environmentData.OnActorUnregistered += delegate { Debug.Log($"Actors: {_environmentData.SceneActors.Count}"); };
    }

    private void Update()
    {
        foreach (SceneActor actor in _environmentData.SceneActors) actor.Act();
    }

    public void CancelActorPreview()
    {
        if (_previewActor != null)
        {
            _previewActor.DestroyActor();
            _previewActor = null;
        }
    }

    public void SpawnActor()
    {
        if (_previewActor != null)
        {
            _previewActor.SetToNormalMode();
            _previewActor.IsActive = true;
            _environmentData.RegisterSceneActor(_previewActor);

            _previewActor = null;
        }
    }

    public void SpawnActorPreview(SceneActor actor)
    {
        _previewActor = _sceneActorFactory.Create(actor.gameObject);
        _previewActor.transform.position = _previewInitPosition;
        _previewActor.Initialize();
        _previewActor.SetToPreviewMode();
        _previewActor.IsActive = false;
    }

    public void UpdatePreviewPosition(Vector2 screenPosition)
    {
        if (_previewActor == null) return;

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane, placementLayerMask))
        {
            _previewActor.PlaceOn(hit.point);
        }
    }


}
