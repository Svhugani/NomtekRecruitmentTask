using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActorsManager 
{
    public void SpawnActorPreview(SceneActor actor);
    public void SpawnActor();
    public void CancelActorPreview();
    public void UpdatePreviewPosition(Vector2 screenPosition);
    public void Act(SceneActor actor);
    public void Interact(SceneActor actor_A, SceneActor actor_B);
}
