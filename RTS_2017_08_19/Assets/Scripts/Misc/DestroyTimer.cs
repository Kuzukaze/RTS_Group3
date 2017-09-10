using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour 
{
    [SerializeField] private bool activated = true;
	[SerializeField] private float lifetime = 13;

	// Use this for initialization
	void Start () 
    {
        if (activated)
		    Destroy (gameObject, lifetime);
	}

    public void Activate()
    {
        Destroy (gameObject, lifetime);
    }
}
