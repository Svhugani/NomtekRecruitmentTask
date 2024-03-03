using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IUIManager
{
    [SerializeField] private ItemSelectionPanel itemSelectionPanel;
    public event Action<GameObject> OnItemSelected;

    private void Awake()
    {
        itemSelectionPanel.OnItemSelected += TriggerActorSelected;
    }

    public void SetupPanels()
    {
        itemSelectionPanel.PopulateGridContent();
    }

    public void AnimateSelectionPanel(bool animateIn)
    {
        if (animateIn) itemSelectionPanel.AnimateIn();
        else itemSelectionPanel.AnimateOut();
    }

    private void TriggerActorSelected(GameObject item)
    {
        OnItemSelected?.Invoke(item);
    }

}
