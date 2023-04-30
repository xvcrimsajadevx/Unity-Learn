using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    [SerializeField] private List<GameObject> waypointArray;

    public Transform GetWaypoint(int waypointIndex)
    {
        return waypointArray[waypointIndex].transform;
    }

    public int GetNextWaypointIndex(int currentWaypointIndex)
    {
        int nextWaypointIndex = currentWaypointIndex + 1;

        if (nextWaypointIndex >= waypointArray.Count)
        {
            nextWaypointIndex = 0;
        }

        return nextWaypointIndex;
    }
}
