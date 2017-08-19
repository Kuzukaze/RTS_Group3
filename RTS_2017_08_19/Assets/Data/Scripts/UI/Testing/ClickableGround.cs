using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableGround : MonoBehaviour {

    private UIManager uiManager;

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    void OnMouseOver ()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit interactionInfo;
            if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
            {
                uiManager.ExecuteCurrentAction(interactionInfo.point);
            }
        }
    }

}
