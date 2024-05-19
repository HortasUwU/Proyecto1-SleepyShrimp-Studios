using UnityEngine;

public class Key : Interactable
{
    protected override void Interact()
    {
        Debug.Log("has cogido la llave");
        GameManager.instance.hasKey = true;
        Destroy(gameObject);
    }
}
