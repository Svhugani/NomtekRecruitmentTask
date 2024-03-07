using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public abstract class SceneActor : MonoBehaviour
{
    protected Rigidbody Rigidbody { get; private set; }
    protected MeshRenderer MeshRenderer { get; private set; }
    public event Action<SceneActor> OnActorDestroy;

    protected EnvironmentData EnvironmentData { get; set; }
    public bool IsActive { get; set; }

    public void Construct(EnvironmentData environmentData)
    {
        this.EnvironmentData = environmentData;
    }

    public virtual void Initialize()
    {
        Rigidbody = GetComponent<Rigidbody>();
        MeshRenderer = GetComponent<MeshRenderer>();
    }

    public virtual void SetToPreviewMode()
    {
        Rigidbody.isKinematic = true;
    }

    public virtual void SetToNormalMode()
    {
        Rigidbody.isKinematic = false;
    }

    public void PlaceOn(Vector3 position)
    {
        transform.position = position + MeshRenderer.bounds.extents.y * Vector3.up;
    }

    public void DestroyActor()
    {
        EnvironmentData.UnregisterSceneActor(this);
        OnActorDestroy?.Invoke(this);
        DOTween.Kill(transform);
        Destroy(this.gameObject);   
    }

    protected abstract void Animate();
    public abstract void Act();
    public abstract void Interact(SceneActor otherActor);

}
