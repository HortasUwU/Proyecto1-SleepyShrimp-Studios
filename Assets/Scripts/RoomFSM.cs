using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFSM : MonoBehaviour
{
    public enum RoomState
    {
        Open,
        Closed,
        Superada
    }

    public RoomState currentState; 
    [SerializeField] private GameObject spawner;
    private Bloqueo bloqueo; 
    public EnemyFSM[] enemigos; // Array para los enemigos

    void Start()
    {
        bloqueo = GetComponentInChildren<Bloqueo>();
        enemigos = GetComponentsInChildren<EnemyFSM>();
        spawner.SetActive(false);

    }

    private void Update()
    {
        switch (currentState)
        {
            case RoomState.Open:
                bloqueo.Abrir();
                break;
            case RoomState.Closed:
                bloqueo.Cerrar();
                spawner.SetActive(true);

                if (TodosEnemigosEliminados())
                {
                    currentState = RoomState.Superada;
                }
                break;
            case RoomState.Superada:
                bloqueo.Abrir();
                GameManager.instance.SaveGameState();
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (currentState == RoomState.Open)
            {
                currentState = RoomState.Closed;
            }
            else if (currentState == RoomState.Superada)
            {
                Debug.Log("Habitación superada");
            }
        }
    }

    private bool TodosEnemigosEliminados()
    {
        foreach (EnemyFSM enemigo in enemigos)
        {
            if (enemigo != null && enemigo.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true; // los enemigos han sido eliminados
    }
}
