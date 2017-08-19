using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float panSpeed = 20f;
    [SerializeField] float panScreeenBorder = 10f;
    [SerializeField] float scrollSpeed =  10f;
    [SerializeField] float minY =  10f;
    [SerializeField] float maxY =  800f;
    [SerializeField] Vector2 panLimit;
	
	void Update ()
    {

        Vector3 pos = transform.position;

        if(Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - panScreeenBorder)
        {
            pos.z += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panScreeenBorder)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width  - panScreeenBorder)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panScreeenBorder)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f* Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        //разные лимиты по z из за угла камеры  в 60. В данном случаи panLimit.y - относится к Z тк Vector2.
        pos.z = Mathf.Clamp(pos.z, (-panLimit.y) * 2, panLimit.y);

      transform.position = pos;
		
	}
}
