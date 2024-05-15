using UnityEngine;

public class Interactuable : MonoBehaviour
{
    public Vector3 InteractuablePosition { get; private set; }

    void Start()
    {
        // Obtener la posición inicial del objeto interactuable
        InteractuablePosition = transform.position;
    }

    public void Interact()
    {
        // Agrega aquí la lógica de interacción específica para el objeto interactuable
        Debug.Log("Interacción con " + gameObject.name);
    }
}

public class Raycast : MonoBehaviour
{
    public Camera camara;
    public GameObject player;
    public LayerMask interactuableLayer;
    public float distanciaMaxima = 10f;

    private void Update()
    {
        // Obtener la posición del jugador
        Vector3 playerPosition = player.transform.position;

        // Realizar un raycast desde la posición de la cámara hacia adelante
        RaycastHit hit;
        if (Physics.Raycast(camara.transform.position, camara.transform.forward, out hit, Mathf.Infinity, interactuableLayer))
        {
            // Comprobar si el objeto golpeado es interactuable
            Interactuable interactuable = hit.collider.GetComponent<Interactuable>();
            if (interactuable != null)
            {
                // Calcular la distancia entre el jugador y el objeto interactuable
                float distancia = Vector3.Distance(playerPosition, interactuable.InteractuablePosition);

                // Verificar si el jugador está dentro de la distancia máxima para interactuar
                if (distancia <= distanciaMaxima)
                {
                    // El jugador está lo suficientemente cerca, realizar la interacción
                    interactuable.Interact();
                }
            }
        }
    }
}
