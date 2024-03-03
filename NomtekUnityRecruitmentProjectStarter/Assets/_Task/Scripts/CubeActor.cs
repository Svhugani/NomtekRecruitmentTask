using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeActor : SceneActor
{

    public override void SetToPreviewMode()
    {
        base.SetToPreviewMode();
        Color color = MeshRenderer.material.color;
        color.a = 0.5f;
        MeshRenderer.material.color = color;
    }

    public override void SetToNormalMode()
    {
        base.SetToNormalMode();
        Color color = MeshRenderer.material.color;
        color.a = 1f;
        MeshRenderer.material.color = color;
        Animate();
    }
    protected override void Animate()
    {
        Rigidbody.AddForce(Vector3.up, ForceMode.Impulse);

        transform.DOScale(Vector3.one * 1.2f, 0.1f)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.OutQuad);
    }
}
