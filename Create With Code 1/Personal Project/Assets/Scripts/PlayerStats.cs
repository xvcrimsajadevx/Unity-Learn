using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int hearts; // 3 hits per heart
    public int totalHearts; // Min 3, max 10

    public int mana; // Current Mana
    public int maxMana; // Max Mana, 100;

    public GameObject equippedItem1;
    public GameObject equippedItem2;

    public GameObject equippedArmour;
    public GameObject equippedWeapon;
    public GameObject equippedShield;
}
