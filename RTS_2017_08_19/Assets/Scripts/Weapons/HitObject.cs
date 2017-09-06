using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObject : MonoBehaviour 
{
    protected ResourceData.Teams team;
    protected bool armed = true;
    private Vector3 originalVelocity;
    [SerializeField] private float projectileSpeed = 40;
    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        originalVelocity = transform.forward * projectileSpeed;
        rb.velocity = originalVelocity;
    }

	void OnCollisionEnter (Collision col) 
    {
        //Debug.Log(col.gameObject);
        Debug.Log(string.Format("Hit {0}", col.gameObject));
        Unit unitHit = col.gameObject.GetComponentInParent<Unit>();
        if (unitHit != null)
        {
            Debug.Log(string.Format("His team {0}, my team {1}", unitHit.Team, team));
            if (unitHit.Team == team)
            {
                Debug.Log("Trying to ignore collision");
                Physics.IgnoreCollision(col.collider, this.GetComponent<Collider>());
                rb.velocity = originalVelocity;
            }
            else
            {
                Debug.Log("Setoff1");
                Setoff(col);
            }
        }
        else
        {
            Debug.Log("Setoff2");
            Setoff(col);
        }
	}

	public virtual void Setoff (Collision col) 
    {

	}

    public void SetArmed (bool val)
    {
        armed = val;
    }

    public void SetTeam(ResourceData.Teams val)
    {
        team = val;
    }
}
