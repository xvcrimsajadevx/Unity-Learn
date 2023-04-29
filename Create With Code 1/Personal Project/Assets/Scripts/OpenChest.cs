using System.Collections;
using UnityEngine;

public class OpenChest : Interactive, IInteract
{
    [SerializeField] private GameObject openState;
    [SerializeField] private GameObject closeState;

    [SerializeField] private bool isLocked;

    // Start is called before the first frame update
    void Start()
    {
        openState.SetActive(false);
        closeState.SetActive(true);
    }

    public void onInteract()
    {
        if (!isLocked)
        {
            openState.SetActive(true);
            closeState.SetActive(false);

            StartCoroutine(deleteChest());
        }
    }

    IEnumerator deleteChest()
    {
        yield return new WaitForSeconds(3f);

        Destroy(gameObject);
    }
}
