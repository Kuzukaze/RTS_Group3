/* Current actions IDs:
    0 : Mover Ground
    1 : Mover Air
    3 : Attack
    4 : Attack Turret
    5 : Raise Shield
    10: Engineer Builder (Engineer)
    11: Engineer Builder (Marine)
    12: Engineer Builder (Apc)
    13: Engineer Builder (Tank)
    14: Engineer Builder (Fighter Jet)
    20: Smart Builder (Factory)
    21: Smart Builder (Hangar)
    22: Smart Builder (Turret)
    23: Smart Builder (Plane Hangar)
    33: Flash
    100: ResourceCollection
    404 : Cancel
    1337: Firework
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BaseActions
{
    Error = -1,
    MoverGround = 0,
    MoverAir = 1,
    Attack = 3,
    AttackTurret = 4,
    RaiseShield = 5,
    SpawnEngineer = 10,
    SpawnMarine = 11,
    SpawnAPC = 12,
    SpawnTank = 13,
    SpawnFighterJet = 14,
    BuildFactory = 20,
    BuildHangar = 21,
    BuildTurret = 22,
    BuildPlaneHangar = 23,
    BuildMatterStorage = 24,
    BuildEnergyStorage = 25,
    Flash = 33,
    ResourceCollection = 100,
    Cancel = 404,
    Firework = 1337
}

public delegate void ActionUnlockHandler(BaseActions eventID);

public class UnlockManager : MonoBehaviour {
    [SerializeField]
    private List<BaseActions> unlockedActions;
    public event ActionUnlockHandler ActionUnlocked;

    void Awake () {
        //unlockedActions = new List<int>();
        BaseAction[] initialActions = FindObjectsOfType<BaseAction>();
        foreach (BaseAction current in initialActions)
        {
            if (!unlockedActions.Contains(current.GetID()) && !current.IsLocked())
            {
                unlockedActions.Add(current.GetID());
                Debug.Log(string.Format("Unlock Manager: Added {0} to unlocked actions", current.GetID()));
            }
        }
	}
        

    public void UnlockAction(BaseActions actionID)
    {
        if (!unlockedActions.Contains(actionID))
        {
            unlockedActions.Add(actionID);
            this.ActionUnlocked(actionID);
        }
        else
        {
            Debug.Log(string.Format("Unlock Manager: Trying to unlock actionID {0}, which is already unlocked", actionID));
        }
    }

    public bool CheckIfLocked(BaseActions actionID)
    {
        if (unlockedActions != null)
        {
            return !unlockedActions.Contains(actionID);
        }
        else
        {
            Debug.Log("Unlock Manager: Error! Unlocked actions list is null, can't check if an action is unlocked!");
            return false;
        }
    }
	
}
