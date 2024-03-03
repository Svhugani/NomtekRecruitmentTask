using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIPanel : MonoBehaviour
{
    [field: SerializeField, Header("Settings: ")] public float FadeInDuration { get; private set; } = .5f;
    [field: SerializeField] public float FadeOutDuration { get; private set; } = .5f;
    protected RectTransform Body { get; private set; }
    protected Vector2 InitAnchor { get; private set; }


    protected virtual void Awake()
    {
        Body = GetComponent<RectTransform>();
        InitAnchor = Body.anchoredPosition;
    }

    public abstract void AnimateIn();
    public abstract void AnimateOut();
}
