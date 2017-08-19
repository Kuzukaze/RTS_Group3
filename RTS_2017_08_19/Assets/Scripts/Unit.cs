using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [SerializeField] private MoverGround movement;
    [SerializeField] private Selectable selectableUnit;
    [SerializeField] private SmartBuilder buildUnit;

    private NavMeshAgent playerNavMesh;


    // private bool IsMouseDown { get; set; }

    private void Start()
    {
        playerNavMesh = GetComponent<NavMeshAgent>();
    }



    void FixedUpdate()
    {
      // Ушло в ClickableGround

      //  Move();
      //  Skill();

  
    }

    private void Update()
    {
        //IsMouseDown = Input.GetMouseButtonDown(1);
    }

    public void slowDown(float speedDown)
    {
        playerNavMesh.speed -= speedDown;
    }

    /*void Move()
    {
        if (IsMouseDown && selectableUnit.IsSelected && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            IsMouseDown = false;
            Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit interactionInfo;
            if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
            {
                movement.ExecuteAction(interactionInfo.point);
            }
        }
        
}*/

    /*
    public void Skill()
    {
        if (Input.GetKeyDown(KeyCode.B) && selectableUnit.IsSelected)
        {
            buildUnit.PlaceHouse();
        }
    }
    */


}
