using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField] private float cameraAngle = 60;
    [SerializeField] private float distance = 10.0f;

    [SerializeField] private Transform target;
    [SerializeField] private float height = 50.0f;
    [SerializeField] private float heightDamping = 2.0f;
    [SerializeField] private float rotationDamping = 3.0f;
    [SerializeField] private float panSpeed = 2.0f;

    void Start()
    {

    }
    
    void LateUpdate()
    {         
        if (!target)
        {
            Debug.LogError("TargetCamera not set");
            return;
        }
        
        //float wantedRotationAngle = target.eulerAngles.y;
        //float currentRotationAngle = transform.eulerAngles.y;
        //currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        //Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        
        float wantedHeight = target.position.y + height;
        float currentHeight = transform.position.y;
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        
        Vector3 panVelocity = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref panVelocity, panSpeed * Time.deltaTime);
        transform.position -= /*currentRotation*/transform.rotation * Vector3.forward * distance;
        
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
        
        //transform.LookAt(target);

    }

    public void SetHeight(float height, float minY, float maxY)
    {
        this.height -= height;
        this.height = Mathf.Clamp(this.height, minY, maxY);

        //this.distance -= height;
        //this.distance = Mathf.Clamp(this.distance, minY, maxY);
    }



}
