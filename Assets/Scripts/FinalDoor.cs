using UnityEngine;

public class FinalDoor : Interactable
{
    protected override void Interact()
    {
        if (GameManager.instance.hasKey)
        {
            // Aquí va la lógica para abrir la puerta final
            Debug.Log("Has abierto la puerta final.");
            this.gameObject.SetActive(false); // Por ejemplo, desactivar la puerta final
        }
        else
        {
            Debug.Log("Necesitas la llave para abrir la puerta final.");
        }
    }
}
