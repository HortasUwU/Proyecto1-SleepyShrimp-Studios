using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Vector3 originalPosition; // Posición original de la puerta
    [SerializeField] public bool isOpen; // Estado de la puerta 

    private void Start()
    {
        originalPosition = transform.position;

        if (isOpen)
        {
            transform.position -= new Vector3(0, 50f, 0);
        }
    }

    // abrir la puerta
    public void OpenDoor()
    {
        if (!isOpen)
        {
            transform.position -= new Vector3(0, 50f, 0);
            isOpen = true;
        }
    }

    // cerrar la puerta
    public void CloseDoor()
    {
        if (isOpen)
        {
            transform.position = originalPosition;
            isOpen = false;
        }
    }
}

