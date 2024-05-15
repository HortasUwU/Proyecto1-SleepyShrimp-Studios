using UnityEngine;

public class Interactuable : MonoBehaviour
{
    public Vector3 InteractuablePosition { get; private set; }

    void Start()
    {
        // Obtener la posici�n inicial del objeto interactuable
        InteractuablePosition = transform.position;
    }

    public void Interact()
    {
        // Agrega aqu� la l�gica de interacci�n espec�fica para el objeto interactuable
        Debug.Log("Interacci�n con " + gameObject.name);
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
        // Obtener la posici�n del jugador
        Vector3 playerPosition = player.transform.position;

        // Realizar un raycast desde la posici�n de la c�mara hacia adelante
        RaycastHit hit;
        if (Physics.Raycast(camara.transform.position, camara.transform.forward, out hit, Mathf.Infinity, interactuableLayer))
        {
            // Comprobar si el objeto golpeado es interactuable
            Interactuable interactuable = hit.collider.GetComponent<Interactuable>();
            if (interactuable != null)
            {
                // Calcular la distancia entre el jugador y el objeto interactuable
                float distancia = Vector3.Distance(playerPosition, interactuable.InteractuablePosition);

                // Verificar si el jugador est� dentro de la distancia m�xima para interactuar
                if (distancia <= distanciaMaxima)
                {
                    // El jugador est� lo suficientemente cerca, realizar la interacci�n
                    interactuable.Interact();
                }
            }
        }
    }
}
