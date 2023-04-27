using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnOff : MonoBehaviour, ITurnOnOff
{
    [SerializeField] private GameObject onState;
    [SerializeField] private GameObject offState;

    [SerializeField] private bool isOn;

    public void turnOnOff(bool switchIsOn)
    {
        if (switchIsOn != isOn)
        {
            isOn = !isOn;

            updateDevice();
        }
    }

    private void updateDevice()
    {
        onState.gameObject.SetActive(isOn);
        offState.gameObject.SetActive(!isOn);
    }
}
