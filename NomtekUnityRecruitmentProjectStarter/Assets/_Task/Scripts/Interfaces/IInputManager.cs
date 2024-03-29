using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputManager
{
    public event Action<Vector2> OnPointerMove;
    public event Action OnPrimaryConfirm;
    public event Action OnSecondaryConfirm;
    public event Action OnCancel;
}
