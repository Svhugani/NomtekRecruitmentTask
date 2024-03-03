using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IUIManager
{
    public event Action<GameObject> OnItemSelected;

    public void AnimateSelectionPanel(bool animateIn)
    {
        throw new NotImplementedException();
    }

    public void SetupPanels()
    {
        throw new NotImplementedException();
    }


}
