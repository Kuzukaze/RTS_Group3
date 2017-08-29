using UnityEngine;
using System.Collections;

public class MiniMapCamera : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Camera minimapCamera;
    [SerializeField] private Collider floorCollider;
    [SerializeField] private Terrain terrain;

    public Vector3 topLeftPosition, topRightPosition, bottomLeftPosition, bottomRightPosition;
    public Vector3 mousePosition;

    private LineRenderer line;

    [SerializeField] float lineWidth = 0.5f;
    [SerializeField] Color color = Color.green;


    public void Start()
    {
        if (this.playerCamera == null)
        {
            Debug.LogError("Unable to determine where the Player Camera component is at.");
        }

        if (this.playerCamera == null)
        {
            Debug.LogError("Unable to determine where the Minimap Camera component is at.");
        }

        if (this.floorCollider == null)
        {
            GameObject floorObject = GameObject.FindGameObjectWithTag("FloorCollider");
            this.floorCollider = floorObject.GetComponent<Collider>();
            if (this.floorCollider == null)
            {
                Debug.LogError("Cannot set Quad floor collider to this variable. Please check.");
            }
        }

        line = this.gameObject.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Sprites/Default"))
        {
            color = this.color
        };
        //line.SetWidth(0.5f, 0.5f);
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.widthMultiplier = lineWidth;

        //line.SetColors(Color.green, Color.green);
        line.startColor = color;
        line.endColor = color;

        //line.SetVertexCount(5);
        //line.numPositions = 5;
        line.positionCount = 5;
    }

    public void Update()
    {
        Ray topLeftCorner = this.playerCamera.ScreenPointToRay(new Vector3(0f, 0f));
        Ray topRightCorner = this.playerCamera.ScreenPointToRay(new Vector3(Screen.width, 0f));
        Ray bottomLeftCorner = this.playerCamera.ScreenPointToRay(new Vector3(0, Screen.height));
        Ray bottomRightCorner = this.playerCamera.ScreenPointToRay(new Vector3(Screen.width, Screen.height));

        RaycastHit[] hits = new RaycastHit[4];
        if (this.floorCollider.Raycast(topLeftCorner, out hits[0], Mathf.Infinity))
        {
            this.topLeftPosition = hits[0].point;
        }
        if (this.floorCollider.Raycast(topRightCorner, out hits[1], Mathf.Infinity))
        {
            this.topRightPosition = hits[1].point;
        }
        if (this.floorCollider.Raycast(bottomLeftCorner, out hits[2], Mathf.Infinity))
        {
            this.bottomLeftPosition = hits[2].point;
        }
        if (this.floorCollider.Raycast(bottomRightCorner, out hits[3], Mathf.Infinity))
        {
            this.bottomRightPosition = hits[3].point;
        }

        line.SetPosition(0, topLeftPosition);
        line.SetPosition(1, topRightPosition);
        line.SetPosition(2, bottomRightPosition);
        line.SetPosition(3, bottomLeftPosition);
        line.SetPosition(4, topLeftPosition);
    }

}