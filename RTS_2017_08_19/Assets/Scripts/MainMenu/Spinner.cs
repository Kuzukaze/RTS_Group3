using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour {

	// Update is called once per frame
	void FixedUpdate () 
    {
        transform.Rotate(0.3f, 0, 0);
	}
}
