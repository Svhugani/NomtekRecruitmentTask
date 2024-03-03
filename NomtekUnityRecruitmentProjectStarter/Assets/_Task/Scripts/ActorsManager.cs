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
    private HashSet<int> _processedCollisions = new HashSet<int>();

    private void Update()
    {
        foreach (SceneActor actor in _sceneActors)
        {
            Act(actor);
        }
    }

    private void FixedUpdate()
    {
        ClearProcessedCollisions();
    }

    public void Act(SceneActor actor)
    {
        EatingBallActor eatingBallActor = actor as EatingBallActor;

        if (eatingBallActor is EatingBallActor && eatingBallActor.IsActive)
        {

            CubeActor closestCube = null;
            float minDist = float.MaxValue;

            foreach (CubeActor cubeActor in _cubeActors)
            {
                float dist = Vector3.Distance(transform.position, cubeActor.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closestCube = cubeActor;
                }
            }

            if (closestCube != null)
            {
                Vector3 dir = closestCube.transform.position - transform.position;
                dir.Normalize();

                eatingBallActor.transform.position = Vector3.Lerp(
                    transform.position,
                    transform.position + dir,
                    eatingBallActor.MovementSpeed * Time.deltaTime);

                eatingBallActor.transform.forward = Vector3.Lerp(
                    transform.forward,
                    dir,
                    eatingBallActor.RotationSpeed * Time.deltaTime);
            }

            eatingBallActor.AnimateRings(Time.deltaTime);
        }
    }

    public void CancelActorPreview()
    {
        if (_previewActor != null)
        {
            DestroyActor(_previewActor);
            _previewActor = null;
        }
    }

    public void SpawnActor()
    {
        if (_previewActor != null)
        {
            _previewActor.SetToNormalMode();
            _sceneActors.Add( _previewActor );

            CubeActor cubeActor = _previewActor as CubeActor;
            if(cubeActor is CubeActor) _cubeActors.Add(cubeActor);

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

    public void Interact(SceneActor actor_A, SceneActor actor_B)
    {
        int collisionHashCode = GetCollisionHashCode(actor_A, actor_B);

        if (_processedCollisions.Contains(collisionHashCode)) return;


        if (actor_A is EatingBallActor && actor_B is CubeActor)
        {
            EatingBallActor eatingBallActor = actor_A as EatingBallActor;
            CubeActor cubeActor = actor_B as CubeActor;


            InteractEatingBallToCube(eatingBallActor, cubeActor);   
        }

        else if (actor_A is CubeActor && actor_B is EatingBallActor)
        {
            EatingBallActor eatingBallActor = actor_B as EatingBallActor;
            CubeActor cubeActor = actor_A as CubeActor;


            InteractEatingBallToCube(eatingBallActor, cubeActor);
        }


        _processedCollisions.Add(collisionHashCode);
    }

    private void InteractEatingBallToCube(EatingBallActor eatingBallActor, CubeActor cubeActor)
    {
        if (cubeActor.IsActive && eatingBallActor.IsActive)
        {
            DestroyActor(cubeActor);

            eatingBallActor.transform.DOScale(Vector3.one * 2.2f, 0.1f)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.OutQuad);
        }
    }

    private int GetCollisionHashCode(SceneActor actor_A, SceneActor actor_B)
    {
        return actor_A.gameObject.GetHashCode() ^ actor_B.gameObject.GetHashCode();
    }

    private void ClearProcessedCollisions()
    {
        _processedCollisions.Clear();
    }


    private void DestroyActor(SceneActor actor)
    {
        CubeActor cubeActor = actor as CubeActor;
        if (cubeActor is CubeActor) _cubeActors.Remove(cubeActor);
        Destroy(actor.gameObject);
    }

}
