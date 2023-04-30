using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour, IInteract
{
    public virtual void OnInteract() { }

    public virtual void OnInteract(GameObject player) { }
}
