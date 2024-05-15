using UnityEngine;
using UnityEngine.AI;

public class ApproachingState : EnemyBaseState
{
    private NavMeshAgent navMeshAgent; // Referencia al componente NavMeshAgent
    private Transform player; // Referencia al transform del jugador

    public override void EnterState(EnemyFSM enemy)
    {
        // Inicia la lógica de Approaching
        Debug.Log("Entering Approaching State");

        // Encuentra la posición del jugador
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Obtiene o agrega un componente NavMeshAgent al enemigo
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            navMeshAgent = enemy.gameObject.AddComponent<NavMeshAgent>();
        }

        // Habilita el NavMeshAgent y establece su destino al jugador
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(player.position);
    }

    public override void UpdateState(EnemyFSM enemy)
    {
        navMeshAgent.SetDestination(player.position);
        // Verifica si el jugador está fuera de rango, en cuyo caso transiciona al estado Idle
        if (!PlayerIsInRange(enemy))
        {
            enemy.TransitionToState(EnemyFSM.EnemyState.Idle);
            return;
        }

        // Si está a la distancia de ataque, transición al estado Attack
        if (DistanceToPlayer(enemy) <= 4f)
        {
            enemy.TransitionToState(EnemyFSM.EnemyState.Attack);
        }
    }

    public override void ExitState(EnemyFSM enemy)
    {
        // Finaliza la lógica de Approaching
        Debug.Log("Exiting Approaching State");

        // Deshabilita el NavMeshAgent
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = false;
        }
    }

    private float DistanceToPlayer(EnemyFSM enemy)
    {
        // Calcula la distancia al jugador
        return Vector3.Distance(enemy.transform.position, player.position);
    }

    private bool PlayerIsInRange(EnemyFSM enemy)
    {
        // Verifica si el jugador está dentro del rango establecido
        return DistanceToPlayer(enemy) <= 10f;
    }
}
