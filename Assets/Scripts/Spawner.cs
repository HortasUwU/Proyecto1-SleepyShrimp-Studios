using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int health;
    private RoomArea roomArea;

    private void Start()
    {
        roomArea = GetComponentInParent<RoomArea>(); // Obtener la referencia al script RoomArea
    }

    private void Update()
    {
        if (health <= 0)
        {
            // Si la salud es igual o menor a cero, eliminar el enemigo y destruir el objeto
            roomArea.EliminarEnemigo(this); // Pasar la referencia al componente Spawner en lugar del GameObject
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bola"))
        {
            Hurt(); // Llamar al método Hurt si el enemigo colisiona con una "Bola"
        }
    }

    private void Hurt()
    {
        health--; // Disminuir la salud del enemigo
    }
}
