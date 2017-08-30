using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour 
{

	[SerializeField]
	private float lifetime = 5;

	// Use this for initialization
	void Start () 
    {
		Destroy (gameObject, lifetime);
	}
}
