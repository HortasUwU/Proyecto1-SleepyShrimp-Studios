using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static GameManager;

public class NotaDisplay : Interactable
{
    public Nota nota;
    public GameObject posIt;
    public TextMeshProUGUI dataText; // Cambiado a TextMeshProUGUI
    public TextMeshProUGUI fechaText; // Cambiado a TextMeshProUGUI

    protected override void Interact()
    {
        dataText.text = nota.data; // asigna el texto de la nota al campo de texto
        fechaText.text = nota.fecha; // asigna la fecha de la nota al campo de texto
        GameManager.instance.Interaction();
        posIt.SetActive(true);
    }
    public void EndInteraction()
    {
        GameManager.instance.currentState = GameState.Playing;
        posIt.SetActive(false);
    }
}
