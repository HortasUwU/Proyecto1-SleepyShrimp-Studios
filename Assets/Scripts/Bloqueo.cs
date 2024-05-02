using UnityEngine;

public class Bloqueo : MonoBehaviour
{
    private Vector3 bloqueado;
    private Vector3 desbloqueado;

    private void Awake()
    {
        bloqueado = transform.position;
        desbloqueado = bloqueado;
        desbloqueado.y -= 50f;

        Abrir();
    }

    public void Abrir()
    {
        transform.position = desbloqueado;
    }

    public void Cerrar()
    {
        transform.position = bloqueado;
    }
}
