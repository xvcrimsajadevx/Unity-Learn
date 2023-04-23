using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private SwitchOptions switchOptions;
    [SerializeField] private SwitchMechanisms switchMechanisms;
    [SerializeField] private SwitchTargets switchTargets;

    int activeOptionIndex;
    public GameObject activeOption;

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
            if (mechanism.GetComponent<SwitchMechanism>().activeTarget != activeOption.name)
            {
                mechanism.GetComponent<SwitchMechanism>().activeTarget = activeOption.name;

                Debug.Log(mechanism.name + ": " + mechanism.GetComponent<SwitchMechanism>().activeTarget);
            }
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
