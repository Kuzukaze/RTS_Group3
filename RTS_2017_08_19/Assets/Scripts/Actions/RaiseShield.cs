using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseShield : BaseAction {
    [SerializeField] private GameObject shield;
    [SerializeField] private float activeTime;
    [SerializeField] private float reloadTime;
    private float stopTime;
    private bool active = false;
    private EventHub eventHub;

    public override void Start()
    {
        base.Start();
        eventHub = FindObjectOfType<EventHub>();
    }

    public override void OnActionStarted()
    {
        shield.SetActive(true);
        active = true;
        stopTime = Time.time + activeTime;
        locked = true;
        protectFromPrematureCompletion = true;
        eventHub.RequestActionPanelUpdate();
    }

    public override void OnActionInProgress()
    {
        if (Time.time > stopTime && active)
        {
            shield.SetActive(false);
            active = false;

        }
        else if (Time.time > stopTime + reloadTime)
        {
            locked = false;
            eventHub.RequestActionPanelUpdate();
            protectFromPrematureCompletion = false;
            CompleteAction();
        }
    }

    public bool IsActive()
    {
        return active;
    }
}
