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

    public override void OnActionStarted(Unit target)
    {
        //Debug.Log("OnActionStarted");
    }

    public override void OnActionInProgress(Unit target)
    {
        shortCounter -= Time.deltaTime;
        //Debug.Log(string.Format("Range = {0}, check if greater than {1}", range,  Vector3.Distance(firePoint1.transform.position, target.transform.position)));
        if (target == null)
        {
            CompleteAction();
            return;
        }
        if (range > Vector3.Distance(firePoints[0].transform.position, targetUnit.transform.position))
        {
            if (LookAtTarget(target.gameObject) && shortCounter <= 0)
            {
                shortCounter = reloadTime;
                Instantiate(projectile, firePoints[currentFirePoint].position, firePoints[currentFirePoint].rotation);
                muzzleFlashes[currentFirePoint].Play(); //muzzleFlashes.Length MUST be equal to firePoints.Length
                currentFirePoint++;
                currentFirePoint = currentFirePoint % firePoints.Length;
            }
        }
        else
        {
            CompleteAction();
            //Debug.Log("in comp");
        }
    }

    public bool LookAtTarget (GameObject lookTarget) 
    {
        Quaternion targetRotation = Quaternion.LookRotation (lookTarget.transform.position - barrel.position); 
        return TurnToDirection (targetRotation);
    }

    public bool TurnToDirection (Quaternion targetRotation)
    {
        Quaternion currentRotation = barrel.transform.rotation;
        Quaternion deltaRotation = Quaternion.Inverse(targetRotation) * currentRotation;
        //Debug.Log (deltaRotation.eulerAngles.z);

        if (Between(deltaRotation.eulerAngles.z, 1, 180))
            RotateTurret(Rotations.Left);
        else if (Between(deltaRotation.eulerAngles.z, 180, 359))
            RotateTurret(Rotations.Right);
        
        if (Between(deltaRotation.eulerAngles.x, 2, 180))
            RotateTurret(Rotations.Up);
        else if (Between(deltaRotation.eulerAngles.x, 180, 358))
            RotateTurret(Rotations.Down);
        //Debug.Log(deltaRotation.eulerAngles.x);
        /*if (Input.GetKey("c"))
            RotateTurret(Rotations.Down);
        else if (Input.GetKey("v"))
            RotateTurret(Rotations.Up);*/

        float rotatingError = Mathf.Abs (targetRotation.eulerAngles.y - currentRotation.eulerAngles.y);
        if (rotatingError < 5)
            return true;
        else
            return false;
    }

    bool Between (float val, float min, float max)
    {
        if (val >= min && val <= max)
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
