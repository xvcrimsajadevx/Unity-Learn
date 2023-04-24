using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private SwitchOptions switchOptions;
    [SerializeField] private SwitchMechanisms switchMechanisms;
    [SerializeField] private SwitchTargets switchTargets;

    private int activeOptionIndex;
    protected GameObject activeOption { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        activeOptionIndex = 0;
        activeOption = switchOptions.switchOptions[activeOptionIndex];
        
        updateMechanisms();

        Debug.Log(activeOption.name);
    }

    // Update is called once per frame
    void Update()
    {
        updateMechanisms();
    }

    private void updateMechanisms()
    {
        foreach (GameObject mechanism in switchMechanisms.mechanisms)
        {
            mechanism.GetComponent<SwitchMechanism>().updateTarget(activeOption);
        }

        foreach (GameObject target in switchTargets.mechanisms)
        {
            target.GetComponent<SwitchTarget>().turnOnOff(activeOption);
        }
    }

    public void onInteract()
    {
        activeOptionIndex++;

        if (activeOptionIndex >= switchOptions.switchOptions.Count)
        {
            activeOptionIndex = 0;
        }

        activeOption = switchOptions.switchOptions[activeOptionIndex];

        Debug.Log(activeOption.name);
    }
}
