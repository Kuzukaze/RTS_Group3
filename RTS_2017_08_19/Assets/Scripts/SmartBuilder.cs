using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartBuilder : BaseAction
{
    [SerializeField] GameObject factoryPrefab;
    [SerializeField] Image ghostImage;
    [SerializeField] Sprite availableBuildSprite;
    [SerializeField] Sprite notAvailableBuildSprite;

    [SerializeField] GameObject ghostPrefab;

    private GameObject currentGhost;

    private float radius = 2f;

    private GameObject factory = null;
    private Selectable selectable;

    public override void Start()
    {   
        base.Start();
        selectable = GetComponent<Selectable>();
    }

    public override void OnActionStarted(Vector3 pos)
    {
        Debug.Log("execute action");
        if (isNoBuildingsNearby(pos))
        {
            //factory = (GameObject)GameObject.Instantiate(factoryPrefab, pos, factoryPrefab.transform.rotation);
            Instantiate(factoryPrefab, pos, factoryPrefab.transform.rotation);
            UnlockAction(actionToUnlock);
        }
        Destroy(currentGhost);
        this.SetShowingGhost(false);
        CompleteAction();
    }

    bool HasMouseMoved()
    {
        return (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0);
    }

    public override void DrawPreActionMarker(Vector3 position)
    { /*
        if (HasMouseMoved())
        {
            Vector3 mousePos = (Input.mousePosition - ghostImage.GetComponent<RectTransform>().localPosition);
            ghostImage.GetComponent<RectTransform>().localPosition = new Vector3(mousePos.x + 15, mousePos.y - 15, mousePos.z);
    
            if (isNoBuildingsNearby(position))
            {
                ghostImage.sprite = availableBuildSprite;
            }
            else
            {
                ghostImage.sprite = notAvailableBuildSprite;
            }
        } */

        if (currentGhost == null)
        {
            currentGhost = Instantiate(ghostPrefab, position, Quaternion.Euler(0,0,0));
        }
        currentGhost.transform.position = position;
        currentGhost.GetComponent<GhostKill>().MarkAsNeeded();
    }
        

    bool isNoBuildingsNearby(Vector3 hitPoint)
    {
        Collider[] hitColliders;
        hitColliders = Physics.OverlapSphere(hitPoint, radius);
        bool isEmpty = true;

        foreach (Collider obj in hitColliders)
        {
            if (obj.tag != "Ground")
            {
                isEmpty = false;
                Debug.Log("Здание!");
            }
        }
        return isEmpty;
    }

}
