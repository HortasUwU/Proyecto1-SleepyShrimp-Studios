using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour 

{
    public Camera playerCamera;
    CharacterController characterController;
    

    public float walkSpeed = 10f;
    Vector3 moveInput = Vector3.zero;

    public float gravityAcceleration = -20f;

    Vector3 rotationInput = Vector3.zero;

    private float cameraVerticalAngle;
    public float rotationSensibilty = 10f;

    public float velocidadRotacion;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        transform.rotation = Quaternion.identity;

    }
    void Update()
    {
        Move();
        Look();
    }

    private void Move()
    {
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveInput = transform.TransformDirection(moveInput) * walkSpeed;


        moveInput.y += gravityAcceleration;
        characterController.Move(moveInput * Time.deltaTime);


    }

    private void Look()
    {
        rotationInput.x = Input.GetAxis("Mouse X") * rotationSensibilty * Time.deltaTime;
        rotationInput.y = Input.GetAxis("Mouse Y") * rotationSensibilty * Time.deltaTime;

        cameraVerticalAngle = cameraVerticalAngle+ rotationInput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70f, 70f);


        transform.Rotate(Vector3.up * rotationInput.x);
        playerCamera.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle, 0f, 0f);


    }
}
