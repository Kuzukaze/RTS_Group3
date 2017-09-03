using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBall : HitObject 
{
    [SerializeField] private float projectileSpeed = 40;

    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * projectileSpeed;
    }


	public override void Setoff (Collision col)
	{
		Explosion exp = GetComponent<Explosion> ();
		exp.SetPosition (transform);
        //exp.SetTeam(team);
		exp.Explode ();
		Destroy (gameObject);
	}
}
