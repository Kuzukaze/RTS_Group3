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

    private NavMeshAgent playerNavMesh;
    private float health;
    private PlayerController playerController;

    private bool isInit = false;

    public TeamInfo Team
    {
        get
        {
            return playerController.Team;
        }
    }
    public PlayerController Player
    {
        get
        {
            return playerController;
        }
    }

    [SerializeField] float lineWidth = 2f;
    [SerializeField] Color color = new Color(0f, 0f, 0.8f, 0.75f);

    // private bool IsMouseDown { get; set; }

    private void Start()
    {
        //Init();
    }

    public void Init(PlayerController player)
    {
        if (!isInit)
        {
            playerNavMesh = GetComponent<NavMeshAgent>();
            health = startHealth;
            playerController = player;
            this.gameObject.GetComponentInChildren<MiniMapSign>().SetColor(playerController.Team.Color);

            isInit = true;
        }
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
        DrawNavLines();
    }

    public void TakeDamage(float damageTake)
    {
        health -= damageTake;
        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            gameObject.transform.position = new Vector3(-47, 2, -47); //TODO: костыль для OnTriggerExit эффектов
            Invoke("Die", 0.03f);
        }
    }

    void Die()
    {
        Destroy(gameObject);
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

    void DrawNavLines()
    {
        NavMeshAgent navAgent = GetComponent<NavMeshAgent>();
        if (navAgent == null || navAgent.path == null)
        {
            return;
        }

        LineRenderer line = this.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = this.gameObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default"))
            {
                color = this.color
            };

            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
            line.widthMultiplier = lineWidth;

            line.startColor = color;
            line.endColor = color;
        }

        NavMeshPath navPath = navAgent.path;
        line.positionCount = navPath.corners.Length;
        for (int i = 0; i < navPath.corners.Length; i++)
        {
            line.SetPosition(i, navPath.corners[i]);
        }

    }

}
