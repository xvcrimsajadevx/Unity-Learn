using System.Collections;
using UnityEngine;

public class OpenChest : Interactive
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

    public override void OnInteract()
    {
        if (!isLocked)
        {
            openState.SetActive(true);
            closeState.SetActive(false);

            StartCoroutine(DeleteChest());
        }
    }

    IEnumerator DeleteChest()
    {
        yield return new WaitForSeconds(3f);

        Destroy(gameObject);
    }
}
