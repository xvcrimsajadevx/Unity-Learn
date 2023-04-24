using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTarget : MonoBehaviour
{
    [SerializeField] private GameObject targetOn;
    [SerializeField] private GameObject targetOff;

    private bool turnOn;
    private bool isOn;

    // Update is called once per frame
    void Update()
    {
        updateTarget();
    }

    public void turnOnOff(GameObject target)
    {
        if (target.gameObject.name == gameObject.name)
        {
            turnOn = true;
        }
        else
        {
            turnOn = false;
        }
    }

    private void updateTarget()
    {
        if (turnOn == isOn) { return; }

        isOn = turnOn;

        if (!isOn)
        {
            targetOn.gameObject.SetActive(false);
            targetOff.gameObject.SetActive(true);
        }
        else
        {
            targetOn.gameObject.SetActive(true);
            targetOff.gameObject.SetActive(false);
        }
    }
}
