using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorUIHolder : MonoBehaviour
{
    [SerializeField] private SOActor soActor;
    [SerializeField] private Actor actor;

    private void Start() {
        actor = new Actor(soActor);
    }

    public void OnAttackAnimationEnd()
    {
        actor.brain.OnAttackAnimationEnd();
    }
}
