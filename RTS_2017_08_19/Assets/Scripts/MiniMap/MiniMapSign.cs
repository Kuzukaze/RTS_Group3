using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapSign : MonoBehaviour
{
    [SerializeField] private Texture signTexture;
    [SerializeField] private int size;

    // Use this for initialization
    void Start()
    {
        Renderer renderer = this.gameObject.GetComponent<Renderer>();
        renderer.material.mainTexture = signTexture;
        this.transform.localScale = Vector3.one * size;
        this.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    public void SetColor(Color color)
    {
        Renderer renderer = this.gameObject.GetComponent<Renderer>();
        renderer.material.color = color;
    }

}
