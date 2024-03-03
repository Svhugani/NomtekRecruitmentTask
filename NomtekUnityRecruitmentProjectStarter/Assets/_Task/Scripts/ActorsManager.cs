using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorsManager : MonoBehaviour, IActorsManager
{
    [SerializeField] private GameObject ground;
    [SerializeField] private LayerMask placementLayerMask;

    private SceneActor _previewActor;
    private List<CubeActor> _cubeActors = new();
    private List<SceneActor> _sceneActors = new();

    private void Update()
    {
        foreach (SceneActor actor in _sceneActors)
        {
            if (actor is EatingBallActor) actor.Act(_cubeActors);
            else actor.Act();
        }
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
            _previewActor = null;
        }
    }

    public void SpawnActorPreview(SceneActor actor)
    {
        _previewActor = Instantiate(actor);
        _previewActor.Initialize();
        _previewActor.SetToPreviewMode();
        _previewActor.IsActive = false;
        _previewActor.OnActorDestroy += ClearReferences;
        _sceneActors.Add(_previewActor);

        CubeActor cubeActor = _previewActor as CubeActor;
        if (cubeActor is CubeActor) _cubeActors.Add(cubeActor);
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

    private void ClearReferences(SceneActor actor)
    {
        CubeActor cubeActor = actor as CubeActor;
        if (cubeActor is CubeActor) _cubeActors.Remove(cubeActor);
        _sceneActors.Remove(actor);
    }

}
