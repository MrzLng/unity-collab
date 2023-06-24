using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerLook : MonoBehaviour
{
    /* Minimum view distance [playerOnly] */

    float minViewDistance = 35f;


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

    float xRotation = -10f;


    /* Jump Velocity [playerOnly] */

    float jVel = 3f;

    private Animator anim;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        anim = playerBody.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        gameObject.GetComponent<Camera>().nearClipPlane = 0.2f;

        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (freeCam)
        {
            anim.SetBool("walk", false);
            gameObject.GetComponent<PositionConstraint>().locked = false;
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
            gameObject.GetComponent<PositionConstraint>().locked = true;
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 10f;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 10f;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, minViewDistance);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
            Vector3 movement = (Vector3.Normalize(new Vector3(transform.forward.x, 0f, transform.forward.z)) * moveInput.y + Vector3.Normalize(new Vector3(transform.right.x, 0f, transform.right.z)) * moveInput.x) * walkSpeed * Time.deltaTime;
            Human.position += movement;
            if (movement != Vector3.zero)
                anim.SetBool("walk", true);
            else anim.SetBool("walk", false);
            if (Input.GetKey(KeyCode.Space) && Human.velocity.y == 0) Human.velocity += new Vector3(0, jVel, 0);
            
        }
    }
}
