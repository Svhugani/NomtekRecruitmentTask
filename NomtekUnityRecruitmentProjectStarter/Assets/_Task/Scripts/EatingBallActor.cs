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

    protected override void Animate()
    {
        transform.DOScale(Vector3.one * 1.1f, 0.15f)
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

        CubeActor cubeActor = collision.collider.GetComponent<CubeActor>();
        if (cubeActor != null && cubeActor.IsActive)
        {
            cubeActor.Interact(this);
            Interact(cubeActor);
        }
    }

    public override void Act(params object[] args)
    {
        if (!IsActive) return;

        if (args.Length > 0 && args[0] is List<CubeActor> cubeTargets)
        {
            CubeActor closestCube = null;
            float minDist = float.MaxValue;

            foreach (CubeActor cubeActor in cubeTargets)
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
                mainRing.Rotate(0, 0, amount * 1.5f);
                secondaryRing.Rotate(0, 0, amount * 1.25f);
                tertiaryRing.Rotate(0, 0, amount * 1.0f);
            }
        }



    }

    public override void Interact(SceneActor otherActor)
    {
        Animate();
    }
}
