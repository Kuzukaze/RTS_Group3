using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vision : MonoBehaviour
{
    public abstract List<Unit> VisibleUnits();
}
