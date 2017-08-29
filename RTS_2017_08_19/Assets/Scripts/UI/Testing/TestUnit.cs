using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestUnit : Unit, IPointerDownHandler {

    [SerializeField] private UIManager uiManager; //this should be redirected to SceneManager to keep IOC.

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            uiManager.ExecuteCurrentAction(this);
        }
    }
}
