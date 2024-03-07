using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingBallActor : SceneActor
{
    [SerializeField, Header("Transform components")] private Transform mainRing;
    [SerializeField] public Transform secondaryRing;
    [SerializeField] public Transform tertiaryRing;
    [SerializeField, Header("Settings")] private float movementSpeed = 2f;
    [SerializeField] private float rotationSpeed = 45f;

    public List<SceneActor> ResourceTargets { get; set; }

    public override void Initialize()
    {
        base.Initialize();
        ActorType = SceneActorType.Eater;
    }

    protected override void Animate()
    {
        transform.DOScale(Vector3.one * 1.1f, 0.10f)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.OutQuad);
    }

    public override void SetToPreviewMode()
    {
        base.SetToPreviewMode();
    }

    public override void SetToNormalMode()
    {
        base.SetToNormalMode();
        Animate();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsActive) return;

        SceneActor actor = collision.collider.GetComponent<SceneActor>();
        if (actor != null && actor.IsActive)
        {
            actor.Interact(this);
            Interact(actor);
        }
    }

    public override void Act()
    {
        if (!IsActive) return;

        if (ResourceTargets != null && ResourceTargets.Count > 0)
        {
            CubeActor closestCube = null;
            float minDist = float.MaxValue;

            foreach (CubeActor cubeActor in ResourceTargets)
            {
                if(!cubeActor.IsActive) continue;

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

                transform.position = Vector3.Lerp(
                    transform.position,
                    transform.position + dir,
                    movementSpeed * Time.deltaTime);

                transform.forward = Vector3.Lerp(
                    transform.forward,
                    dir,
                    rotationSpeed * Time.deltaTime);

                // Rings manual animation part (visual effect only)
                float amount = Time.deltaTime * rotationSpeed;
                mainRing.Rotate(0, 0, amount * 2.5f);
                secondaryRing.Rotate(0, 0, amount * 2.25f);
                tertiaryRing.Rotate(0, 0, amount * 2.0f);
            }

        }

    }

    public override void Interact(SceneActor otherActor)
    {
        Animate();
    }
}
