using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Money
    public int currency;

    // Potions
    public int healthPotion;
    public int manaPotion;
    public int greatPotion;

    // Exploration
    public int smallKeys;
    public int largeKeys;

    public bool map;

    // Player Advancement
    public int heartPieces;

    // Items
    public int bombs;
    public int bolts;

    // Equipment
    public bool Armour1;
    public bool Armour2;
    public bool Armour3;

    public bool Shield1;
    public bool Shield2;
    public bool Shield3;

    public int swordLevel;

    public bool pickaxe;
    public bool lantern;
    public bool crossbow;
}
