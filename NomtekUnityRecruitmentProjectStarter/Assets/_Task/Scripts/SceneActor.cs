using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class SceneActor : MonoBehaviour
{
    protected Rigidbody Rigidbody { get; private set; }
    protected MeshRenderer MeshRenderer { get; private set; }
    public event Action<SceneActor> OnActorDestroy;

    public bool IsActive { get; set; }


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
        OnActorDestroy?.Invoke(this);
        Destroy(this.gameObject);   
    }

    protected abstract void Animate();
    public abstract void Act(params object[] args);
    public abstract void Interact(SceneActor otherActor);
}
