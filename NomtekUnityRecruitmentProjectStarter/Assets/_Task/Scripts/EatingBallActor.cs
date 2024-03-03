using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingBallActor : SceneActor
{
    [SerializeField, Header("Transform components")] private Transform mainRing;
    [SerializeField] public Transform secondaryRing;
    [SerializeField] public Transform tertiaryRing;
    [field: SerializeField, Header("Settings")] public float MovementSpeed { get; private set; } = 1f;
    [field: SerializeField] public float RotationSpeed { get; private set; } = 90;

    protected override void Animate()
    {
        transform.DOScale(Vector3.one * 1.8f, 0.15f)
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
        CubeActor cubeActor = collision.collider.GetComponent<CubeActor>();
        if (cubeActor != null && cubeActor.IsActive)
        {
            TriggerOnActorCollide(this, cubeActor);
        }
    }

    public void AnimateRings(float dt)
    {
        float amount = dt * RotationSpeed;
        mainRing.Rotate(0, 0, amount * 1.5f);
        secondaryRing.Rotate(0, 0, amount * 1.25f);
        tertiaryRing.Rotate(0, 0, amount * 1.0f);
    }

}
