using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActorsManager : MonoBehaviour, IActorsManager
{
    [SerializeField] private GameObject ground;
    [SerializeField] private LayerMask placementLayerMask;

    private SceneActor _previewActor;
    private List<SceneActor> _resourceActors = new();
    private List<SceneActor> _sceneActors = new();

    private readonly Vector3 _previewInitPosition = new Vector3(100, 0, 0);

    private void Update()
    {
        foreach (SceneActor actor in _sceneActors) actor.Act();
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
        _previewActor = Instantiate(actor, _previewInitPosition, Quaternion.identity);
        _previewActor.Initialize();
        _previewActor.SetToPreviewMode();
        _previewActor.IsActive = false;
        _previewActor.OnActorDestroy += ClearReferences;
        _sceneActors.Add(_previewActor);

        if(_previewActor.ActorType == SceneActorType.Resource)
        {
            _resourceActors.Add(_previewActor);
        }

        else
        { 
            EatingBallActor eatingBallActor = _previewActor as EatingBallActor;
            if (eatingBallActor != null) 
            {
                eatingBallActor.ResourceTargets = _resourceActors;
            }
        }
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
        _sceneActors.Remove(actor);

        if (actor.ActorType == SceneActorType.Resource) 
        {
            _resourceActors.Remove(actor);
        }
    }

}
