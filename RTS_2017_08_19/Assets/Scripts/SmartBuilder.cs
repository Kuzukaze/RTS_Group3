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

    private float radius = 2f;

    private GameObject factory = null;
    private Selectable selectable;

    void Start()
    {
        selectable = GetComponent<Selectable>();
    }

    public override void ExecuteAction(Vector3 pos)
    {
        if (isNoBuildingsNearby(pos))
        {
            factory = (GameObject)GameObject.Instantiate(factoryPrefab, pos, factoryPrefab.transform.rotation);
        }
    }

    bool HasMouseMoved()
    {
        return (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0);
    }

    public void DrawGhostBuilding(Vector3 position)
    {
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
        }
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
