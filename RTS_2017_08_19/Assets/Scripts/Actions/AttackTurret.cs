using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTurret : Attack {

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform platform;
    [SerializeField] private Transform barrel;

    [SerializeField] Transform[] firePoints;
    [SerializeField] ParticleSystem[] muzzleFlashes;
    private int currentFirePoint = 0;
    private ResourceData.Teams team;

    public override void OnActionStarted(Unit target)
    {
        team = GetComponent<Unit>().Team;
    }

    public override void OnActionInProgress(Unit target)
    {
        shortCounter -= Time.deltaTime;
        if (target == null)
        {
           // Debug.Log("Target is null");
            CompleteAction();
            return;
        }
        if (range > Vector3.Distance(firePoints[0].transform.position, targetUnit.transform.position))
        {
            if (LookAtTarget(target.gameObject) && shortCounter <= 0)
            {
                shortCounter = reloadTime;
                GameObject instantiated = Instantiate(projectile, firePoints[currentFirePoint].position, firePoints[currentFirePoint].rotation);
                instantiated.GetComponent<HitObject>().SetTeam(team);
                foreach (Collider current in this.GetComponentsInChildren<Collider>())
                {
                    Physics.IgnoreCollision(instantiated.GetComponent<Collider>(), current);
                }
                muzzleFlashes[currentFirePoint].Play(); //muzzleFlashes.Length MUST be equal to firePoints.Length
                currentFirePoint++;
                currentFirePoint = currentFirePoint % firePoints.Length;
            }
        }
        else
        {
            //Debug.Log(string.Format("Distance: {0}, Range: {1}",Vector3.Distance(firePoints[0].transform.position, targetUnit.transform.position),range));
            CompleteAction();
            //Debug.Log("in comp");
        }
    }

    public override void OnActionComplete()
    {
        if (platform != null && barrel != null)
        {
            platform.transform.rotation = platform.parent.transform.rotation;
            barrel.transform.rotation = platform.parent.transform.rotation;
        }
    }

    public bool LookAtTarget (GameObject lookTarget) 
    {
        float rotSpeed = 360f; 
        Vector3 D = lookTarget.transform.position - barrel.transform.position;  
        Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(D), rotSpeed * Time.deltaTime);

        platform.transform.eulerAngles = new Vector3(0, rot.eulerAngles.y,0); 
        barrel.transform.eulerAngles = new Vector3(rot.eulerAngles.x, rot.eulerAngles.y, 0);
        return LooksAtTarget(lookTarget);
    }

    public bool LooksAtTarget (GameObject lookTarget)
    {
        Quaternion targetRotation = Quaternion.LookRotation (lookTarget.transform.position - barrel.position); 
        Quaternion currentRotation = barrel.transform.rotation;
        float rotatingError = Mathf.Abs (targetRotation.eulerAngles.y - currentRotation.eulerAngles.y);
        if (rotatingError < 5)
            return true;
        else
            return false;
    }


    enum Rotations {Left, Right, Up, Down}

    void RotateTurret(Rotations dir)
    {
        Quaternion rot;
        switch (dir)
        {
            case Rotations.Left:
                rot = Quaternion.Euler(platform.rotation.eulerAngles + new Vector3(0, -3, 0));
                platform.rotation = rot;
                break;
            case Rotations.Right:
                rot = Quaternion.Euler(platform.rotation.eulerAngles + new Vector3(0, 3, 0));
                platform.rotation = rot;
                break;
            case Rotations.Up:
                rot = Quaternion.Euler(barrel.rotation.eulerAngles + new Vector3(-0.5f, 0, 0));
                barrel.rotation = rot;
                break;
            case Rotations.Down:
                rot = Quaternion.Euler(barrel.rotation.eulerAngles + new Vector3(0.5f, 0, 0));
                barrel.rotation = rot;
                break;
        }
    }
}
