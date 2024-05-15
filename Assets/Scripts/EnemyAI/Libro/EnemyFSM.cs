using UnityEngine;
using System;
using System.Collections;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Approaching,
        Attack,
        Stunned
    }

    private EnemyBaseState currentState;
    [SerializeField] private int health;

    void Start()
    {
        TransitionToState(EnemyState.Idle);
    }

    void Update()
    {
        // Actualiza el estado actual
        currentState.UpdateState(this);
    }

    public void TransitionToState(EnemyState nextState)
    {
        // Sale del estado actual
        if (currentState != null)
        {
            currentState.ExitState(this);
        }

        // Cambia al nuevo estado
        currentState = GetStateInstance(nextState);

        // Entra en el nuevo estado
        currentState.EnterState(this);
    }

    private EnemyBaseState GetStateInstance(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle:
                return new IdleState();
            case EnemyState.Approaching:
                return new ApproachingState();
            case EnemyState.Attack:
                return new AttackState();
            case EnemyState.Stunned:
                return new StunnedState();
            default:
                return null;
        }
    }

    // Actualiza el estado actual
    public void UpdateCurrentState()
    {
        currentState.UpdateState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bola"))
        {
            Hurt();
        }
    }

    private void Hurt()
    {
        health--; 
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyFSM enemy);
    public abstract void UpdateState(EnemyFSM enemy);
    public abstract void ExitState(EnemyFSM enemy);
}
