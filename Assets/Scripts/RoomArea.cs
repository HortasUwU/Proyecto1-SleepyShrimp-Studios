using UnityEngine;

public class RoomArea : MonoBehaviour
{
    private Bloqueo bloqueo; // Referencia al componente Bloqueo
    private Spawner[] enemigos; // Array para almacenar los enemigos

    private bool hasEntered = false;
    private int noEnemys;

    private void Start()
    {
        bloqueo = GetComponentInChildren<Bloqueo>();
        enemigos = GetComponentsInChildren<Spawner>();
        noEnemys = enemigos.Length;
        foreach (var enemigo in enemigos)
        {
            enemigo.gameObject.SetActive(false); // Activar el GameObject del enemigo
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasEntered)
            {
                Debug.Log("Entró en la habitación");
                hasEntered = true;
                bloqueo.Cerrar();
                activateEnemys();
            }
            else
            {
                Debug.Log("Ya estuvo aquí");
            }
        }
    }

    public void Update()
    {
        if (noEnemys <= 0)
        {
            if (bloqueo != null)
            {
                bloqueo.Abrir(); // Abrir el bloqueo cuando no haya más enemigos
            }
        }
    }

    void activateEnemys()
    {
        foreach (var enemigo in enemigos)
        {
            enemigo.gameObject.SetActive(true); // Activar el GameObject del enemigo
        }
    }

    public void EliminarEnemigo(Spawner enemigo)
    {
        // Reducir el contador de enemigos y abrir el bloqueo si no hay más enemigos
        noEnemys--;
        if (noEnemys <= 0 && bloqueo != null)
        {
            bloqueo.Abrir(); // Abrir el bloqueo cuando no haya más enemigos
        }
    }
}
