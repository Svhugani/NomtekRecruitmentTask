using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActorsManager 
{
    public void SpawnActorPreview(SceneActor actor);
    public void SpawnActor();
    public void CancelActorPreview();
    public void UpdatePreviewPosition(Vector2 screenPosition);
}
