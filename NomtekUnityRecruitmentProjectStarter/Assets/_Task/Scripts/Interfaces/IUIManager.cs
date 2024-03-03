using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIManager
{
    public void SetupPanels();
    public void AnimateSelectionPanel(bool animateIn);
    public event Action<GameObject> OnItemSelected;

}
