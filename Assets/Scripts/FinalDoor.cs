using UnityEngine;

public class FinalDoor : Interactable
{
    protected override void Interact()
    {
        if (GameManager.instance.hasKey)
        {
            // Aqu� va la l�gica para abrir la puerta final
            Debug.Log("Has abierto la puerta final.");
            this.gameObject.SetActive(false); // Por ejemplo, desactivar la puerta final
        }
        else
        {
            Debug.Log("Necesitas la llave para abrir la puerta final.");
        }
    }
}
