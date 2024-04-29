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
    }
    void Update()
    {
        Move();
        Look();
        LookAtMouse();


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
    
    void LookAtMouse()
    {
        // Obtener la posición del ratón en el mundo
        Vector3 posicionRaton = Input.mousePosition;
        posicionRaton.z = 10; // Distancia desde la cámara al plano de la pantalla

        // Convertir la posición del ratón de pantalla a un rayo en el mundo
        Vector3 posicionEnElMundo = Camera.main.ScreenToWorldPoint(posicionRaton);

        // Calcular la dirección hacia la posición del ratón
        Vector3 direccion = (posicionEnElMundo - transform.position).normalized;

        // Calcular la rotación para mirar hacia la posición del ratón
        Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);

        // Rotar gradualmente hacia la rotación deseada
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, velocidadRotacion * Time.deltaTime);
    }


}
