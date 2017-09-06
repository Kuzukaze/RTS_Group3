using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBall : HitObject 
{

	public override void Setoff (Collision col)
	{
		Explosion exp = GetComponent<Explosion> ();
		exp.SetPosition (transform);
        exp.SetTeam(team);
        //exp.SetTeam(team);
		exp.Explode ();
		Destroy (gameObject);
	}
}
