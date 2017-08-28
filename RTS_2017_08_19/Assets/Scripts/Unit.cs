using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [SerializeField] private MoverGround movement;
    [SerializeField] private Selectable selectableUnit;
    [SerializeField] private SmartBuilder buildUnit;
    [SerializeField] private float startHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private string team;

    private NavMeshAgent playerNavMesh;
    private float health;
    //private bool dieInLateUpdate = false;


    // private bool IsMouseDown { get; set; }

    private void Start()
    {
        playerNavMesh = GetComponent<NavMeshAgent>();
        health = startHealth;
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

    public void TakeDamage(float damageTake)
    {
        health -= damageTake;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            gameObject.transform.position = new Vector3(-47, 2, -47); //костыль для OnTriggerExit эффектов
            Invoke("Die", 0.03f);
        }



    }

    void Die()
    {
        FindObjectOfType<EventHub>().SignalUnitDeath(this);
        Destroy(gameObject);
        //dieInLateUpdate = true;
    }
    /*
    void LateUpdate()
    {
        if (dieInLateUpdate)
            Destroy(gameObject);
    }*/

    public string GetTeam()
    {
        return team;
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
