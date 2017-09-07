using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisabler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.Find("HealthUI").gameObject.SetActive(false);
        transform.Find("MiniMapSign").gameObject.SetActive(false);
        if (GetComponent<LineRenderer>() != null)
            GetComponent<LineRenderer>().enabled = false;
	}

}
