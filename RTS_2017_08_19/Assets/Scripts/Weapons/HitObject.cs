using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObject : MonoBehaviour 
{

    protected bool armed = true;
    /*
    protected Teams team = Teams.BadGuys;


    public void SetTeam(Teams val)
    {
        team = val;
        Debug.Log(string.Format("projectile team changed to {0}",team));
    } */
	

	void OnCollisionEnter (Collision col) 
    {
        Debug.Log(col.gameObject);
		Setoff (col);
	}

	public virtual void Setoff (Collision col) 
    {

	}

    public void SetArmed (bool val)
    {
        armed = val;
    }
}
