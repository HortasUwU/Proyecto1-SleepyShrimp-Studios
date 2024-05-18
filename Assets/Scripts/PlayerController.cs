using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    CharacterController characterController;

    public float walkSpeed = 10f;
    public float gravityAcceleration = -20f;

    Vector3 moveInput = Vector3.zero;
    Vector3 rotationInput = Vector3.zero;

    private float cameraVerticalAngle;
    public float rotationSensibility = 10f;

    private void Awake()
    {
        GameObject playerSpawner = GameObject.FindWithTag("PlayerSpawner");
        if (playerSpawner != null)
        {
            transform.position = playerSpawner.transform.position;
            transform.rotation = Quaternion.identity;
        }
        else
        {
            Debug.LogError("No se encontró un objeto con la etiqueta 'PlayerSpawner'.");
        }

        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        transform.rotation = Quaternion.identity;

    }

    void Update()
    {
        if (GameManager.instance.currentState == GameManager.GameState.GameOver)
        {
            tpSpawner();

        }
        if (GameManager.instance.currentState == GameManager.GameState.Playing)
        {
            Move();
            Look();
        }


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
        rotationInput.x = Input.GetAxisRaw("Mouse X") * rotationSensibility * Time.deltaTime;
        rotationInput.y = Input.GetAxisRaw("Mouse Y") * rotationSensibility * Time.deltaTime;

        cameraVerticalAngle += rotationInput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70f, 70f);

        transform.Rotate(Vector3.up * rotationInput.x);
        playerCamera.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle, 0f, 0f);
    }


    public void tpSpawner()
    {
        GameObject playerSpawner = GameObject.FindWithTag("PlayerSpawner");
        if (playerSpawner != null)
        {
            characterController.enabled = false;
            transform.position = playerSpawner.transform.position;
            transform.rotation = Quaternion.identity;
            characterController.enabled = true;
        }
        else
        {
            Debug.LogError("No se encontró un objeto con la etiqueta 'PlayerSpawner'.");
        }
    }

}
