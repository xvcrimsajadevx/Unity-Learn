using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Health : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int health;

    public void OnTakeDamage(int damage)
    {
        Destroy(gameObject);
    }
}
