using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AppManager : MonoBehaviour, IAppManager
{

    [Inject] public IInputManager InputManager { get; private set; }
    [Inject] public IActorsManager ActorsManager { get; private set; }
    [Inject] public IUIManager UIManager { get; private set; }


    private void Start()
    {
        InitializeScene();
    }

    public void InitializeScene()
    {
        UIManager.SetupPanels();
        UIManager.AnimateSelectionPanel(true);
        UIManager.OnItemSelected += StartPlacementProcedure;
    }

    private void StartPlacementProcedure(GameObject item)
    {
        SceneActor actor = item.GetComponent<SceneActor>();
        if (actor == null) return;

        ActorsManager.SpawnActorPreview(actor);
        UIManager.AnimateSelectionPanel(false);
        InputManager.OnPointerMove += ActorsManager.UpdatePreviewPosition;
        InputManager.OnPrimaryConfirm += FinishPlacementProcedure;
        InputManager.OnCancel += CancelPlacementProcedure;

    }

    public void FinishPlacementProcedure()
    {
        ActorsManager.SpawnActor();
        InputManager.OnPrimaryConfirm -= FinishPlacementProcedure;
        UIManager.AnimateSelectionPanel(true);
    }

    private void CancelPlacementProcedure()
    {
        ActorsManager.CancelActorPreview();
        InputManager.OnCancel -= CancelPlacementProcedure;
        UIManager.AnimateSelectionPanel(true);
    }
}
