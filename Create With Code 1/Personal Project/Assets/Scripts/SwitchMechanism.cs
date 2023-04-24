using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchMechanism : Interactive, IInteract
{
    [SerializeField] private GameObject switchController;
    [SerializeField] private List<GameObject> switchOptions = new List<GameObject>();

    protected string activeTarget { get; private set; }
    private string activeSwitch;

    private void Update()
    {
        updateSwitch();
    }

    private void updateSwitch()
    {
        if (activeSwitch == null)
        {
            activeSwitch = activeTarget;
        }

        if (activeSwitch != activeTarget)
        {
            activeSwitch = activeTarget;

            foreach (GameObject option in switchOptions)
            {
                if (option.name == activeSwitch)
                {
                    option.SetActive(true);
                }
                else
                {
                    option.SetActive(false);
                }
            }
        }
    }

    public void updateTarget(GameObject activeOption)
    {
       if (activeTarget != activeOption.name)
        {
            activeTarget = activeOption.name;
        }
    }

    public void onInteract()
    {
        switchController.GetComponent<SwitchController>().onInteract();
    }
}
