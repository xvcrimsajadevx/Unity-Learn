using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanismReciever : MonoBehaviour, IOnHit
{
    [SerializeField] protected List<GameObject> targetMechanisms;
    [SerializeField] protected List<GameObject> triggerMechanisms;

    public virtual void OnHit() { }

    public virtual void OnHit(GameObject activeTarget) { }
}
