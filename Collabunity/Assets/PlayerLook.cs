using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    /* Minimum view distance [playerOnly] */

    [SerializeField] float minViewDistance = 25f;


    /* Model of the player / human */

    [SerializeField] Transform playerBody;


    /* Default walking speed of the human */

    [SerializeField] float walkSpeed = 5f;


    /* Rigidbody of the human */

    [SerializeField] Rigidbody Human;


    /* If true, the camera moves in a freecam mode [playerOnly] */

    [SerializeField] bool freeCam = false;
    [SerializeField] float difference;


    /* Movement input of the player [playerOnly] */

    Vector2 moveInput;


    /* Mouse sensitivity for camera movement [playerOnly] */

    public float mouseSensitivity = 100f;


    /* X rotation of the player [playerOnly] */

    float xRotation = 0f;


    /* Jump Velocity [playerOnly] */

    [SerializeField] float jVel = 5f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!freeCam && (transform.position.x != Human.position.x || transform.position.z != Human.position.z)) transform.position = Human.position + new Vector3(0f, difference, 0f);

        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (freeCam)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            transform.eulerAngles += new Vector3(-mouseY, mouseX, 0);
            transform.position += transform.forward * moveInput.y * walkSpeed * Time.deltaTime;
            transform.position += transform.right * moveInput.x * walkSpeed * Time.deltaTime;
            // Move the camera forward, backward, left, and right
            transform.position += transform.forward * moveInput.y * walkSpeed * Time.deltaTime;
            transform.position += transform.right * moveInput.x * walkSpeed * Time.deltaTime;
        }
        else
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 10f;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 10f;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, minViewDistance);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
            Vector3 movement = (Vector3.Normalize(new Vector3(transform.forward.x, 0f, transform.forward.z)) * moveInput.y + Vector3.Normalize(new Vector3(transform.right.x, 0f, transform.right.z)) * moveInput.x) * walkSpeed * Time.deltaTime;
            Human.position += movement;

            if (Input.GetKey(KeyCode.Space) && Human.velocity.y == 0) Human.velocity += new Vector3(0, jVel, 0);
            
        }
    }
}
