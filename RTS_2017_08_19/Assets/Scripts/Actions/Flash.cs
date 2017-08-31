using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : BaseAction
{
    [SerializeField]Transform firePoint;
    [SerializeField]FlashController flash;
    [SerializeField]float flashSpeed;

    private float timer;


    private FlashController newFlash;

    public override void Start()
    {
        base.Start();
    }

    public override void OnActionStarted(Vector3 pos)
    {
        timer = 0;
        newFlash = Instantiate(flash, firePoint.position, firePoint.rotation);
        newFlash.flashSpeed = flashSpeed;
    }

    public override void OnActionInProgress(Vector3 pos)
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            Debug.Log("Comp FLR");
            CompleteAction();
            Destroy(newFlash.gameObject, 0.2f);
        }

    }
}
