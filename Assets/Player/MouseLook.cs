using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [HideInInspector] public Transform playerBody;
    Player player;
    float xRotation = 0f;

    /// <summary>
    /// This locks the mouse on the unity gameplay screen
    /// </summary>
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.Find("Player").GetComponent<Player>();
    }


    void Update()
    {
        Inputs();
    }

    /// <summary>
    /// This is how the player can look around
    /// </summary>
    public void Inputs()
    {
        float mouseX = Input.GetAxis("Mouse X") * player.mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * player.mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
