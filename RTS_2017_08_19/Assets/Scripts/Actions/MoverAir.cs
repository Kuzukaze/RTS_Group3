using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverAir : BaseAction 
{
    [SerializeField] private Transform altimeter;
    [SerializeField] private float targetAltitude;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    private Rigidbody rb;
    private Vector3 destination;

    public override void OnActionStarted(Vector3 pos)
    {
        destination = pos + new Vector3(0, targetAltitude, 0);
        rb = GetComponent<Rigidbody>();
    }

    public override void OnActionInProgress(Vector3 pos)
    {
        //Debug.Log(Vector3.Distance(transform.position, destination));
        if (Vector3.Distance(transform.position, destination) < 7)
        {
            CompleteAction();
        }
    }

    public override void OnActionInProgressFixed(Vector3 pos)
    {
        Vector3 forceVector = (destination - transform.position).normalized;
        forceVector.y = 0.0f;
        rb.AddForce(forceVector.normalized * acceleration);
        float currentAltitude = CurrentAltitude();
        if (currentAltitude < targetAltitude)
        {
            rb.AddForce(Vector3.up * acceleration/3.0f);
        }
        else if (currentAltitude > targetAltitude + 3)
        {
            rb.AddForce(Vector3.down * acceleration/3.0f);
        } 
        Debug.Log(Mathf.Clamp(maxSpeed - maxSpeed*(1 - Vector3.Distance(transform.position, destination)/targetAltitude*2),1,maxSpeed));
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, Mathf.Clamp(maxSpeed - maxSpeed*(1 - Vector3.Distance(transform.position, destination)/(targetAltitude*2)),1,maxSpeed)); 
        transform.forward = Vector3.Lerp(transform.forward, rb.velocity, Time.deltaTime/2);
    }

    public override void OnActionComplete()
    {
        rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.Euler( new Vector3(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
    }

    float CurrentAltitude()
    {
        RaycastHit hit;
        if (Physics.Raycast(altimeter.position, Vector3.down, out hit))
        {
            return Vector3.Distance(hit.point, altimeter.position);
        }
        else
            return -1;
    }

}
