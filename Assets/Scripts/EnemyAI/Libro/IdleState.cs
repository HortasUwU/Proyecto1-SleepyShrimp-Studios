using UnityEngine;

public class IdleState : EnemyBaseState
{
    private float idleRange = 10f; // Rango m�nimo para detectar al jugador y cambiar de estado
    private Transform player; // Referencia al transform del jugador
    private bool PlayerIsInRange(EnemyFSM enemy)
    {
        // Comprueba si el jugador est� dentro de un rango determinado
        float distanceToPlayer = Vector3.Distance(enemy.transform.position, player.position);
        return distanceToPlayer <= idleRange;
    }

    public override void EnterState(EnemyFSM enemy)
    {
        // Inicia la l�gica de Idle
        Debug.Log("Entering Idle State");

        // Encuentra la posici�n del jugador
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void UpdateState(EnemyFSM enemy)
    {
        Vector3 directionToPlayer = (player.position - enemy.transform.position).normalized;

        if (PlayerIsInRange(enemy))
        {
            enemy.TransitionToState(EnemyFSM.EnemyState.Approaching);
        }
    }

    public override void ExitState(EnemyFSM enemy)
    {
        Debug.Log("Exiting Idle State");
    }
}

