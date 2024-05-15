using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedState : EnemyBaseState
{
    private float stunDuration = 2f; // Duraci�n del aturdimiento en segundos
    private float timer; // Temporizador para controlar la duraci�n del aturdimiento

    public override void EnterState(EnemyFSM enemy)
    {
        // Inicializa el estado de aturdimiento
        Debug.Log("Entering Stunned State");

        // Reinicia el temporizador
        timer = 0f;

        // Det�n el movimiento del enemigo
        enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public override void UpdateState(EnemyFSM enemy)
    {

        // Incrementa el temporizador
        timer += Time.deltaTime;

        // Si el temporizador supera la duraci�n del aturdimiento
        if (timer >= stunDuration)
        {
            // Transici�n al estado Idle
            enemy.TransitionToState(EnemyFSM.EnemyState.Idle);
        }
    }

    public override void ExitState(EnemyFSM enemy)
    {
        // Finaliza el estado de aturdimiento
        Debug.Log("Exiting Stunned State");
    }
}
