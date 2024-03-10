using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnvData", menuName = "ScriptableObjects/EnvironmentData", order = 1)]
public class EnvironmentData : ScriptableObject
{
    public List<SceneActor> SceneActors { get; private set; }
    public List<CubeActor> CubeActors { get; private set; }
    public List<EatingBallActor> EatingBallActors { get; private set; }

    public Action<SceneActor> OnActorRegistered;
    public Action<SceneActor> OnActorUnregistered;

    private void OnEnable()
    {
        SceneActors = new List<SceneActor>();
        CubeActors = new List<CubeActor>();
        EatingBallActors = new List<EatingBallActor>();
    }

    public void RegisterSceneActor(SceneActor actor)
    {
        SceneActors.Add(actor);

        if (actor is CubeActor) 
        {
            CubeActors.Add(actor as CubeActor);
        }

        else if (actor is EatingBallActor)
        {
            EatingBallActors.Add(actor as EatingBallActor);
        }

        OnActorRegistered?.Invoke(actor);
    }

    public void UnregisterSceneActor(SceneActor actor)
    {
        SceneActors.Remove(actor);

        if (actor is CubeActor)
        {
            CubeActors.Remove(actor as CubeActor);
        }

        else if (actor is EatingBallActor)
        {
            EatingBallActors.Remove(actor as EatingBallActor);
        }

        OnActorUnregistered?.Invoke(actor);
    }

}
