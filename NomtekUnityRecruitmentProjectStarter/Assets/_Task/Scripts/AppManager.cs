using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour, IAppManager
{
    public IInputManager InputManager => throw new System.NotImplementedException();

    public IActorsManager ActorsManager => throw new System.NotImplementedException();

    public IUIManager UIManager => throw new System.NotImplementedException();

    public void InitializeScene()
    {
        throw new System.NotImplementedException();
    }
}
