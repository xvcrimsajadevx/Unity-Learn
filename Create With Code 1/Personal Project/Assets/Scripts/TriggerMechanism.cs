using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMechanism : MonoBehaviour, IOnHit
{
    [SerializeField] private MechanismReciever controller;
    [SerializeField] private List<GameObject> switchOptions = new List<GameObject>();

    public void onHit()
    {
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            controller.GetComponent<MechanismReciever>().onHit();
        }
    }

    public void updateMechanism(int activeIndex)
    {
        foreach (GameObject option in switchOptions)
        {
            if (option.gameObject != switchOptions[activeIndex])
            {
                option.gameObject.SetActive(false);
            }
            else
            {
                option.gameObject.SetActive(true);
            }
        }
    }
    
    public void updateMechanism(bool isOn)
    {
        switchOptions[0].gameObject.SetActive(isOn);
        switchOptions[1].gameObject.SetActive(!isOn);
    }
}
