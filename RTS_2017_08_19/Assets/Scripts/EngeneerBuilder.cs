using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngeneerBuilder : BaseAction
{
    [SerializeField]GameObject engineerPrefab;
    private GameObject engineer = null;

    private Selectable selectable;

    private float radius = 0.2f;

    void Start()
    {
        selectable = GetComponent<Selectable>();
    }

    public override void ExecuteAction(Vector3 pos)
    {
        if (isNoBuildingsNearby(pos))
        {
            engineer = (GameObject)GameObject.Instantiate(engineerPrefab, pos, engineerPrefab.transform.rotation);
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
