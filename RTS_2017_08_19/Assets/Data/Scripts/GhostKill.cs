using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostKill : MonoBehaviour {

    private bool isNeeded = false;

    public void MarkAsNeeded()
    {
        isNeeded = true;
    }

    void LateUpdate()
    {
        if (!isNeeded)
            Destroy(this.gameObject);
        else
            isNeeded = false;
    }
}
