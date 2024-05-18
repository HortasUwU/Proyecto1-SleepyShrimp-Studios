using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFSM : MonoBehaviour
{
    public enum RoomState
    {
        Open,
        Closed,
        Superada,
        Empty
    }

    public RoomState currentState;

    [SerializeField] private GameObject spawner;
    private Bloqueo bloqueo;
    private EnemyFSM[] enemigos; // Array para los enemigos
    private bool enemigosObtenidos = false; // Bandera para verificar si los enemigos han sido obtenidos
    private bool flagSuperada = true;

    void Start()
    {
        bloqueo = GetComponentInChildren<Bloqueo>();
        spawner.SetActive(true);
    }

    private void Update()
    {
        switch (currentState)
        {
            case RoomState.Open:
                bloqueo.Abrir();
                spawner.SetActive(false);
                break;
            case RoomState.Closed:
                bloqueo.Cerrar();
                spawner.SetActive(true);
                if (!enemigosObtenidos)
                {
                    ObtenerEnemigos();
                }
                if (enemigos != null && TodosEnemigosEliminados())
                {
                    currentState = RoomState.Superada;
                }
                if (!TodosEnemigosEliminados() && GameManager.instance.currentState == GameManager.GameState.GameOver)
                {
                    ResetearEstado();
                }
                break;
            case RoomState.Superada:
                if (flagSuperada == true)
                {
                    bloqueo.Abrir();
                    GameManager.instance.SaveGameState();
                    flagSuperada = false;
                }
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

    private void ObtenerEnemigos()
    {
        enemigos = GetComponentsInChildren<EnemyFSM>();
        enemigosObtenidos = true;
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
        return true;
    }

    public void ResetearEstado()
    {
        if (!TodosEnemigosEliminados())
        {
            currentState = RoomState.Open;
            enemigosObtenidos = false;
        }
    }
}
