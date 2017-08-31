using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour 
{
    [SerializeField]
    protected float radius = 5.0F;

    [SerializeField]
    protected float power = 1000.0F;

	[SerializeField]
	protected float baseDamage = 20.0F;

    [SerializeField]
    protected ParticleSystem ps;


    //protected Teams team = Teams.GoodGuys;

    public void Explode()
    {
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders)
		{
			Rigidbody rb = hit.GetComponent<Rigidbody>();

			if (rb != null && rb.tag != "Player" && rb.tag != "Projectile")
				ExplosionPhysicsEffect (rb);
            /*
			Health health = hit.GetComponent<Health> ();

			if (health != null) 
			{
                TeamMember targetTeam = hit.gameObject.GetComponent<TeamMember>();
				Vector3 direction = rb.transform.position - transform.position;
                Debug.Log(string.Format("team = {0} targetTeam = {1}", team, targetTeam.Team()));
                if (!(ignorePlayer && rb.tag == "Player") && targetTeam.Team() != team)
					health.TakeDamage(baseDamage - baseDamage*(direction.magnitude/radius));
			}*/
            Unit unit = hit.gameObject.GetComponent<Unit>();
            if (unit != null)
            {
                unit.TakeDamage(baseDamage - baseDamage * ((Vector3.Distance(rb.transform.position, transform.position)) / radius));
            }
		}
		ParticleSystem particle = Instantiate (ps, explosionPos, Quaternion.Euler(Vector3.forward)); //Vector3.forward
        particle.transform.Rotate (new Vector3 (-90,0,0));
		particle.Play ();
    }

	public virtual void ExplosionPhysicsEffect (Rigidbody rb)
	{

	}

    public void SetPosition (Transform trans) 
    {
        transform.position = trans.position;
    }

    public void SetPosition (Vector3 trans) 
    {
        transform.position = trans;
    }

    public void SetRotation (Vector3 rot)
    {
        transform.rotation = Quaternion.Euler(rot);
    }
    /*
    public void SetTeam(Teams val)
    {   string.Format("Explosion team is being set to = {0}", val);
        team = val;
    } */
}
