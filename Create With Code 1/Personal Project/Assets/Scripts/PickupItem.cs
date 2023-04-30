using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour, IPickup
{
    enum ItemType
    {
        RecoveryItem,
        PowerUp,
        InventoryItem,
        TreasureItem,
        EquipmentUpgrade
    }

    [SerializeField] ItemType itemType;

    public void OnPickup()
    {
        Debug.Log("Collected " + itemType + ": " + gameObject.name);

        Destroy(gameObject);
    }
}
