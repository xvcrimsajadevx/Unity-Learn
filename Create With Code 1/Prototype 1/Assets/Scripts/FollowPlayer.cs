using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private Vector3 offset1 = new Vector3(0, 7, -9);
    private Vector3 offset2 = new Vector3(0, 5, 0);

    private Vector3 currentOffset;

    // Start is called before the first frame update
    void Start()
    {
        currentOffset = offset1;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetButtonDown("SwitchCamera"))
        {
            if (currentOffset == offset1)
            {
                currentOffset = offset2;
            }
            else
            {
                currentOffset = offset1;
            }
        }
        
        // Offsets camera behind the player by adding to player's position
        transform.position = Player.transform.position + currentOffset;
    }
}
