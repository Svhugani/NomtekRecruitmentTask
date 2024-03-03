using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorsManager : MonoBehaviour, IActorsManager
{
    [SerializeField] private GameObject ground;
    [SerializeField] private LayerMask placementLayerMask;

    private SceneActor _previewActor;

    public void CancelActorPreview()
    {
        if (_previewActor != null)
        {
            Destroy(_previewActor.gameObject);
            _previewActor = null;
        }
    }

    public void SpawnActor()
    {
        if (_previewActor != null)
        {
            _previewActor.SetToNormalMode();
            _previewActor = null;
        }
    }

    public void SpawnActorPreview(SceneActor actor)
    {
        _previewActor = Instantiate(actor);
        _previewActor.Initialize();
        _previewActor.SetToPreviewMode();
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
