using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private static DoorManager instance; // Instancia singleton

    public static DoorManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DoorManager>(); // Busca la instancia en la escena si no hay ninguna asignada
                if (instance == null)
                {
                    GameObject obj = new GameObject(); // Crea un nuevo GameObject
                    obj.name = "DoorManager"; // Nombre del GameObject
                    instance = obj.AddComponent<DoorManager>(); // Añade el componente DoorManager al GameObject
                }
            }
            return instance;
        }
    }

    private DoorController[] doors;
    private bool doorsOpen;

    private void Awake()
    {
        // Si hay una instancia previa y no es la misma que la actual, destruye esta instancia
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this; // Establece esta instancia como la instancia singleton
            DontDestroyOnLoad(this.gameObject); // Evita que el GameObject se destruya al cargar una nueva escena
        }
    }

    void Start()
    {
        // Encontrar todos los objetos con el componente DoorController en la escena
        DoorController[] foundDoors = FindObjectsOfType<DoorController>();

        // Inicializar el array doors con el tamaño adecuado
        doors = new DoorController[foundDoors.Length];

        // Copiar los elementos encontrados al array doors
        for (int i = 0; i < foundDoors.Length; i++)
        {
            doors[i] = foundDoors[i];
        }
    }

    public void ToggleDoors()
    {
        // Iterar sobre todas las puertas y cambiar su estado
        foreach (DoorController door in doors)
        {
            DoorController doorController = door.GetComponent<DoorController>();
            doorsOpen = doorController.isOpen;

            if (doorController != null)
            {
                // Alternar el estado de la puerta
                if (doorsOpen)
                {
                    doorController.CloseDoor();
                }
                else
                {
                    doorController.OpenDoor();
                }
            }
        }

        // Cambiar el estado de las puertas
        doorsOpen = !doorsOpen;
    }
}
