using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearController : MechanismReciever
{
    protected int activeObjectIndex;

    private void Start()
    {
        UpdateMechanisms();
    }

    void Update()
    {
        if (activeObjectIndex < 0 || activeObjectIndex >= triggerMechanisms.Count)
        {
            activeObjectIndex = 0;
        }
    }

    public override void OnHit()
    {
        activeObjectIndex++;

        if (activeObjectIndex >= triggerMechanisms.Count)
        {
            activeObjectIndex = 0;
        }

        UpdateMechanisms();
    }

    private void UpdateMechanisms()
    {
        foreach (GameObject mechanism in triggerMechanisms)
        {
            mechanism.GetComponent<TriggerMechanism>().UpdateMechanism(activeObjectIndex);
        }

        foreach (GameObject mechanism in targetMechanisms)
        {
            if (mechanism == targetMechanisms[activeObjectIndex])
            {
                mechanism.GetComponent<ITurnOnOff>().turnOnOff(true);
            }
            else
            {
                mechanism.GetComponent<ITurnOnOff>().turnOnOff(false);
            }
        }
    }
}
