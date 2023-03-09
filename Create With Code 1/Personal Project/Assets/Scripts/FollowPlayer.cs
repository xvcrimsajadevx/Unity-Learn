using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject[] Cameras;

    private GameObject currentCamera;

    // Start is called before the first frame update
    void Start()
    {
        // Sets action view camera on start
        currentCamera = Cameras[0];
    }

    // Update is called once per frame
    void Update()
    {
        CameraNullCheck();

        SwitchCamera();

        // Offsets camera behind the player by adding to player's position
        transform.position = Player.transform.position;
    }

    private void SwitchCamera()
    {
        // Switches between action view and personal view cameras
        if (Input.GetButtonDown("SwitchCamera"))
        {
            if (currentCamera == Cameras[0])
            {
                // Deactivates action view camera and turns on personal view camera
                currentCamera.SetActive(false);
                currentCamera = Cameras[1];
                currentCamera.SetActive(true);
            }
            else
            {
                // Deactivates personal view camera and turns on action view camera
                currentCamera.SetActive(false);
                currentCamera = Cameras[0];
                currentCamera.SetActive(true);
            }
        }
    }

    private void CameraNullCheck()
    {
        // Activates action view camera if current camera is null
        if (currentCamera == null)
        {
            currentCamera = Cameras[0];

            foreach (GameObject camera in Cameras)
            {
                // Activates action view camera and turns all other cameras off
                if (camera == currentCamera)
                {
                    camera.SetActive(true);
                }
                else
                {
                    camera.SetActive(false);
                }
            }   
        }
    }
}
