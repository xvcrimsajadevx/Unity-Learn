using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMechanism : MonoBehaviour, IOnHit
{
    [SerializeField] private MechanismReciever controller;
    [SerializeField] private List<GameObject> switchOptions;

    public void OnHit()
    {
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            controller.GetComponent<MechanismReciever>().OnHit();
        }
    }

    public void UpdateMechanism(int activeIndex)
    {
        foreach (GameObject option in switchOptions)
        {
            if (option.gameObject != switchOptions[activeIndex])
            {
                option.SetActive(false);
            }
            else
            {
                option.SetActive(true);
            }
        }
    }
    
    public void UpdateMechanism(bool isOn)
    {
        switchOptions[0].SetActive(isOn);
        switchOptions[1].SetActive(!isOn);
    }
}
