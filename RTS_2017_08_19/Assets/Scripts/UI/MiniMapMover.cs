using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniMapMover : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private float width;
    private float height;

    private float paddingX;
    private float paddingY;

    private float terrainWidth;
    private float terrainHeight;

    [SerializeField] private Transform miniMapTarget;
    [SerializeField] private Terrain terrain;

    private bool isMouseDown = false;

    // Use this for initialization
    void Start()
    {
        GridLayoutGroup gridLG = this.gameObject.GetComponentInParent<GridLayoutGroup>();
        Vector2 rect = gridLG.cellSize;
        width = rect.x;
        height = rect.y;

        paddingX = gridLG.padding.left + gridLG.padding.right;
        paddingY = gridLG.padding.top + gridLG.padding.bottom;

        Vector3 terrainSize = terrain.terrainData.size;
        terrainWidth = terrainSize.x;
        terrainHeight = terrainSize.z;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(Input.GetMouseButtonDown(0))
        {
            UpdateCameraTargetPosition(eventData);
        }
        if(Input.GetMouseButtonDown(1))
        {
            //TODO: default command on minimap RMB click
        }
    }
    

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            UpdateCameraTargetPosition(eventData);
        }
    }

    private void UpdateCameraTargetPosition(PointerEventData eventData)
    {
        float xPos = (eventData.position.x - paddingX) / width;
        float yPos = (eventData.position.y - paddingY) / height;

        xPos *= terrainWidth;
        yPos *= terrainHeight;

        //Debug.Log("Set MiniMap on " + xPos + "_" + yPos);

        miniMapTarget.position = new Vector3(xPos, miniMapTarget.position.y, yPos);
    }
}
