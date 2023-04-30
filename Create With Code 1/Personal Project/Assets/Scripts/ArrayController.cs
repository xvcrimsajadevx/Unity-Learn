using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class ArrayController : MonoBehaviour, ITurnOnOff
{
    [SerializeField] private List<GameObject> arrayItems;

    [SerializeField] private bool isOn;

    public void turnOnOff(bool switchIsOn)
    {
        if (switchIsOn != isOn)
        {
            isOn = !isOn;

            TurnArrayOnOff();
        }
    }

    private void TurnArrayOnOff()
    {
        foreach (GameObject item in arrayItems)
        {
            item.GetComponent<TurnOnOff>().turnOnOff(isOn);
        }
    }
}
