using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool isSwitch;
    [SerializeField] private bool isBreakable;
    [SerializeField] private bool isTrigger;

    public bool isOn;
    private bool isTriggered = false;

    public void OnInteract()
    {
        if (isSwitch)
        {
            isOn = !isOn;
        }

        if (isBreakable)
        {
            Destroy(gameObject);
        }

        if (isTrigger)
        {
            isTriggered = true;
        }
    }
}