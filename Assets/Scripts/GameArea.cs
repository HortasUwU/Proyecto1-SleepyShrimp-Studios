using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        // Verificar si el objeto que sale del área tiene el tag "Player", "Enemy" o "Bola"
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Bola"))
        {
            // Destruir el objeto que sale del área
            Destroy(other.gameObject);
        }
    }
}