using UnityEngine;
using static GameManager;

public class DispararBola : MonoBehaviour
{
    [SerializeField] private AudioClip lanzar;

    public GameObject bolaPrefab;
    public Camera camara;
    public new MeshRenderer renderer;
    public float fuerzaDisparo = 20f;
    public float distanciaBola;


    private GameObject bolaLanzada; // Almacena la referencia al objeto de la bola clonada
    Vector3 puntoCentroPantalla = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

    void Update()
    {
        if (bolaLanzada == null && Input.GetButtonDown("Fire1") && isPlayin()) // Solo permitir lanzar la bola si no hay ninguna bola lanzada actualmente
        {
            Disparar();
        }

        if (Input.GetButtonDown("Fire1") && isPlayin()) // Verificar si el botón derecho del ratón fue presionado
        {
            Ray rayo2 = camara.ScreenPointToRay(puntoCentroPantalla);
            RaycastHit hit;
            float distancia = Vector3.Distance(transform.position, bolaLanzada.transform.position);
            if (distancia <= distanciaBola)
            {
                if (Physics.Raycast(rayo2, out hit))
                {
                    if (hit.collider.gameObject == bolaLanzada)
                    {
                        Destroy(bolaLanzada); // Destruir la bola que ha sido lanzada
                        bolaLanzada = null; // Establecer la referencia a la bola lanzada como nula
                        renderer.enabled = true;
                    }
                }
            }

        }
    }

    void Disparar()
    {
        renderer.enabled = false;

        // Lanzar un rayo desde la cámara en la dirección del punto en el centro de la pantalla
        Ray rayo = camara.ScreenPointToRay(puntoCentroPantalla);

        // Calcular la dirección del disparo hacia el punto en el centro de la pantalla
        Vector3 direccionDisparo = rayo.direction;

        //Reproducir sonido
        AudioManager.Instance.PlaySound(lanzar);

        // Instanciar la bola
        bolaLanzada = Instantiate(bolaPrefab, transform.position, Quaternion.identity);

        // Aplicar fuerza al rigidbody de la bola en la dirección calculada
        Rigidbody rb = bolaLanzada.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direccionDisparo * fuerzaDisparo, ForceMode.Impulse);
        }
    }
    public bool isPlayin()
    {
        // Obtener la instancia del GameManager
        GameManager gameManager = GameManager.instance;

        // Verificar si la instancia es válida y el estado del juego es "Playing"
        if (gameManager != null && gameManager.currentState == GameManager.GameState.Playing)
        {
            return true; // El juego está en curso
        }
        else
        {
            return false; // El juego no está en curso
        }
    }

    public void recallBall()
    {
        Destroy(bolaLanzada); // Destruir la bola que ha sido lanzada
        bolaLanzada = null; // Establecer la referencia a la bola lanzada como nula
        renderer.enabled = true;
    }
}
