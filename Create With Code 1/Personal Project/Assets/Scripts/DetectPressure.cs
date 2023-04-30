using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPressure : MonoBehaviour
{
    [SerializeField] private GameObject[] activeStates;
    [SerializeField] private float resistance;
    public bool staysActive;

    public bool isActive;

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            activeStates[0].SetActive(true);
            activeStates[1].SetActive(false);
        }
        else
        {
            if (!staysActive)
            {
                activeStates[0].SetActive(false);
                activeStates[1].SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.GetComponent<Rigidbody>())
        {
            return;
        }

        if (other.gameObject.GetComponent<Rigidbody>().mass >= resistance)
        {
            isActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isActive = false;
    }
}
