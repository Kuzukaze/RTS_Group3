using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : BaseAction
{
    [SerializeField] ParticleSystem particle;
    public override void OnActionStarted()
    {
        particle.Play();
        CompleteAction();
    }

}
