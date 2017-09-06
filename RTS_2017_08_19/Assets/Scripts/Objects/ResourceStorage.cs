using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    [SerializeField] private ResourceData.ResourceType resourceType;
    [SerializeField] private uint storageCapacity;
    [SerializeField] private bool capResource = false;
    private PlayerController player;
    
    // Use this for initialization
    private void Start()
    {
        player = this.GetComponent<Unit>().Player;

        player.AddResourceStorage(this, resourceType);
        player.GetResourceByType(resourceType).MaxIncrease((int)storageCapacity);
        if (capResource)
        {
            player.GetResourceByType(resourceType).Add((int)storageCapacity);
        }
    }
    

    private void OnDestroy()
    {
        if (capResource)
        {
            player.GetResourceByType(resourceType).Use((int)storageCapacity);  //TODO: test situation when all energy is used, but we destroy storage -> energy value goes < 0 (!)
        }
        player.GetResourceByType(resourceType).MaxDecrease((int)storageCapacity);
        player.RemoveResourceStorage(this, resourceType);
    }
}
