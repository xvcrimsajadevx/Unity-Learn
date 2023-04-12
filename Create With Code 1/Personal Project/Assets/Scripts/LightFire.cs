using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightFire : Interactable
{
    [SerializeField] private GameObject fireOrb;

    private void Update()
    {
        if (fireOrb.activeSelf != isOn)
        {
            fireOrb.SetActive(isOn);
        }
    }
}
