using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushExplosion : Explosion 
{

	public override void ExplosionPhysicsEffect (Rigidbody rb)
	{
		//rb.AddExplosionForce(power, transform.position, radius, 3.0F);
	}

}
