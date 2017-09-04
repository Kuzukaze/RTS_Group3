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
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthBar;

    private NavMeshAgent playerNavMesh;
    //private bool dieInLateUpdate = false;
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
    

    [SerializeField] float lineWidth = 0.8f;
    [SerializeField] Color color = new Color(0f, 1f, 1f, 0.75f);

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
            healthBar.fillAmount = health / maxHealth;
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

    public void Heal(float healingAmount)
    {
        health += healingAmount;
        healthBar.fillAmount = health / maxHealth;

        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public void TakeDamage(float damageTake)
    {
        health -= damageTake;
        healthBar.fillAmount = health / maxHealth;

        if (health <= 0)
        {
            //gameObject.transform.position = new Vector3(-47, 2, -47); //TODO: костыль для OnTriggerExit эффектов
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


    public void slowDown(float speedDown)
    {
        playerNavMesh.speed -= speedDown;
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

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
