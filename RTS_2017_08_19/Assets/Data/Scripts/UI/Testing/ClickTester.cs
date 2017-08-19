using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickTester : MonoBehaviour, IPointerDownHandler {

    //[SerializeField]
    private UIManager uiManager;

    void Start ()
    {
        uiManager = GetComponentInParent<UIManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            uiManager.ExecuteCurrentAction(new Vector3(0, 0, 0));
        }
    }
}
