using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void UnitDeathHandler(Unit killedUnit);
public delegate void ActionPanelUpdateRequestHandler();

public delegate void ResourceChangeHandler(int value);
public delegate void ResourceWarnHandler(bool isMaxed);

public class EventHub : MonoBehaviour
{

    public event UnitDeathHandler UnitDeathEvent;

    public event ActionPanelUpdateRequestHandler ActionPanelUpdateRequest;
    public event ResourceChangeHandler MatterCurrentChangeEvent;
    public event ResourceChangeHandler MatterMaxChangeEvent;
    public event ResourceChangeHandler EnergyCurrentChangeEvent;
    public event ResourceChangeHandler EnergyMaxChangeEvent;
    public event ResourceWarnHandler MatterMaxed;
    public event ResourceWarnHandler EnergyMaxed;
    public event ResourceWarnHandler MatterLow;
    public event ResourceWarnHandler EnergyLow;


    public void SignalUnitDeath(Unit killedUnit)
    {
        Debug.Log("EventHub: Signaling units death");
        if (UnitDeathEvent != null)
        {
            UnitDeathEvent(killedUnit);
        }
    }

    public void SignalMatterCurrentChanged(int value)
    {
        Debug.Log("EventHub: Signaling matter changed to " + value);
        if (MatterCurrentChangeEvent != null)
        {
            MatterCurrentChangeEvent(value);
        }
    }
    public void SignalMatterMaxChanged(int value)
    {
        Debug.Log("EventHub: Signaling matter max changed to " + value);
        if (MatterMaxChangeEvent != null)
        {
            MatterMaxChangeEvent(value);
        }
    }
    public void SignalEnergyCurrentChanged(int value)
    {
        Debug.Log("EventHub: Signaling energy changed to " + value);
        if (EnergyCurrentChangeEvent != null)
        {
            EnergyCurrentChangeEvent(value);
        }
    }
    public void SignalEnergyMaxChanged(int value)
    {
        Debug.Log("EventHub: Signaling energy max changed to " + value);
        if (EnergyMaxChangeEvent != null)
        {
            EnergyMaxChangeEvent(value);
        }
    }

    public void SignalMatterMaxed(bool isMaxed)
    {
        Debug.Log("EventHub: Signaling matter maxed:  " + isMaxed);
        if (MatterMaxed != null)
        {
            MatterMaxed(isMaxed);
        }
    }
    public void SignalEnergyMaxed(bool isMaxed)
    {
        Debug.Log("EventHub: Signaling energy maxed:  " + isMaxed);
        if (EnergyMaxed != null)
        {
            EnergyMaxed(isMaxed);
        }
    }
    public void SignalMatterCriticallyLow(bool isLow)
    {
        Debug.Log("EventHub: Signaling matter low:  " + isLow);
        if (MatterLow != null)
        {
            MatterLow(isLow);
        }
    }
    public void SignalEnergyCriticallyLow(bool isLow)
    {
        Debug.Log("EventHub: Signaling energy low:  " + isLow);
        if (EnergyLow != null)
        {
            EnergyLow(isLow);
        }
    }

    public void RequestActionPanelUpdate ()
    {
        if (ActionPanelUpdateRequest != null)
            ActionPanelUpdateRequest();
    }

}
