using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTorch : Interactive, IInteract
{

    [SerializeField] private GameObject fireObject;
    [SerializeField] private bool fireLit;
    [SerializeField] private bool staysLit;

    private void Start()
    {
        fireObject.SetActive(fireLit);
    }

    public void onInteract()
    {
        if (fireLit && staysLit) { return; }

        fireLit = !fireLit;
        fireObject.SetActive(fireLit);
    }
}
