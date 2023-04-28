using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleController : MechanismReciever
{
    private bool isOn;
    
    // Start is called before the first frame update
    void Start()
    {
        isOn = true;
        turnOnOff();
    }

    public override void onHit()
    {
        isOn = !isOn;
        turnOnOff();
    }

    private void turnOnOff()
    {
        foreach (GameObject mechanism in triggerMechanisms)
        {
            mechanism.GetComponent<TriggerMechanism>().updateMechanism(isOn);
        }

        foreach (GameObject mechanism in targetMechanisms)
        {
            mechanism.GetComponent<ITurnOnOff>().turnOnOff(isOn);
        }
    }
}