using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanismReciever : MonoBehaviour, IOnHit
{
    [SerializeField] protected List<GameObject> targetMechanisms = new List<GameObject>();
    [SerializeField] protected List<GameObject> triggerMechanisms = new List<GameObject>();

    public virtual void onHit() { }

    public virtual void onHit(GameObject activeTarget) { }
}
