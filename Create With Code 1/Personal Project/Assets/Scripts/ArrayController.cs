using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;

public class ArrayController : MonoBehaviour, ITurnOnOff
{
    [SerializeField] private List<GameObject> arrayItems = new List<GameObject>();

    [SerializeField] private bool isOn;

    public void turnOnOff(bool switchIsOn)
    {
        if (switchIsOn != isOn)
        {
            isOn = !isOn;

            turnArrayOnOff();
        }
    }

    private void turnArrayOnOff()
    {
        foreach (GameObject item in arrayItems)
        {
            item.gameObject.GetComponent<TurnOnOff>().turnOnOff(isOn);
        }
    }
}
