using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    private int jumpForce = 10;
    private Transform player; // Referencia al transform del jugador
    private float attackCooldown = 5f; // Tiempo de enfriamiento entre ataques
    private float lastAttackTime = -Mathf.Infinity; // Último momento en que se realizó un ataque
    private Rigidbody rb;

    public override void EnterState(EnemyFSM enemy)
    {
        rb = enemy.GetComponent<Rigidbody>();
        // Inicia la lógica de Attack
        Debug.Log("Entering Attack State");

        // Encuentra la posición del jugador
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void UpdateState(EnemyFSM enemy)
    {
        // Verifica si ha pasado el tiempo suficiente desde el último ataque
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            // Realiza el ataque
            PerformAttack(enemy);

            // Actualiza el tiempo del último ataque
            lastAttackTime = Time.time;
        }
    }

    private void PerformAttack(EnemyFSM enemy)
    {
        Vector3 directionToPlayer = (player.position - enemy.transform.position).normalized;
        
        // Orienta al enemigo hacia el jugador
        enemy.transform.LookAt(player);

        // Aplica una fuerza hacia adelante para simular el ataque
        rb.AddForce(directionToPlayer * jumpForce, ForceMode.Impulse);

        // Transiciona al estado Stunned
        enemy.TransitionToState(EnemyFSM.EnemyState.Stunned);
    }

    public override void ExitState(EnemyFSM enemy)
    {
        // Finaliza la lógica de Attack
        Debug.Log("Exiting Attack State");
    }
}
