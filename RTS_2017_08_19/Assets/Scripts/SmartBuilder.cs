using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartBuilder : BaseAction
{
    [SerializeField]GameObject factoryPrefab;
    private GameObject factory = null;

    private Selectable selectable;

    private float radius = 2f;

    void Start()
    {
        selectable = GetComponent<Selectable>();
    }

    /* Перенял на себя ExecuteAction.
     
      Ставим домик на землю по указанию курсора мыши, проверяя, не заденет ли он дома вокруг.
    public void PlaceHouse()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f) && hit.collider.CompareTag("Ground"))
        {
            if (isNoBuildingsNearby(hit.point))
            {
                factory = (GameObject)GameObject.Instantiate(factoryPrefab, hit.point, factoryPrefab.transform.rotation);
            }
        }
    }
    */

    public override void ExecuteAction(Vector3 pos)
    {
        if (isNoBuildingsNearby(pos))
        {
            factory = (GameObject)GameObject.Instantiate(factoryPrefab, pos, factoryPrefab.transform.rotation);
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
