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
    [SerializeField] Vector2 panLimitMin;
    [SerializeField] Vector2 panLimitMax;
    [SerializeField] private FollowCamera followCamera;

    void LateUpdate ()
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

        float heightDiff = scroll * scrollSpeed * 100f * Time.deltaTime;
        followCamera.SetHeight(heightDiff, minY, maxY);

        //pos.y -= scroll * scrollSpeed * 100f* Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, panLimitMin.x, panLimitMax.x);
        //pos.y = Mathf.Clamp(pos.y, minY, maxY);
        //разные лимиты по z из за угла камеры  в 60. В данном случае panLimit.y - относится к Z тк Vector2.
        pos.z = Mathf.Clamp(pos.z, (panLimitMin.y), panLimitMax.y);

      transform.position = pos;
		
	}
}
