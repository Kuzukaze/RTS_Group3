using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void UnitDeathHandler(Unit killedUnit);
public delegate void ActionPanelUpdateRequestHandler();

public class EventHub : MonoBehaviour {

    public event UnitDeathHandler UnitDeathEvent;
    public event ActionPanelUpdateRequestHandler ActionPanelUpdateRequest;

    public void SignalUnitDeath (Unit killedUnit)
    {
        Debug.Log("EventHub: Signaling units death");
        if (UnitDeathEvent != null)
            UnitDeathEvent(killedUnit);
    }

    public void RequestActionPanelUpdate ()
    {
        if (ActionPanelUpdateRequest != null)
            ActionPanelUpdateRequest();
    }

}
