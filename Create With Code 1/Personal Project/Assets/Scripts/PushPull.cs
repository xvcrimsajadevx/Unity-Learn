using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPull : Interactive
{
    [SerializeField] private GameObject pushPullHandler;

    public void RotatePushPull(Transform focusTransform)
    {
        pushPullHandler.transform.rotation = focusTransform.rotation;
    }
    public override void OnInteract(GameObject player)
    {
        Debug.Log("In the " + gameObject.name + "OnInteract method");

        
    }
}
