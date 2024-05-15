using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static GameManager;

public class NotaDisplay : MonoBehaviour
{
    public Nota nota;
    public TextMeshProUGUI dataText; // Cambiado a TextMeshProUGUI
    public TextMeshProUGUI fechaText; // Cambiado a TextMeshProUGUI

    [SerializeField] private int distanciaInteraccion = 10;
    public GameObject posIt;
    public Camera cam;

    [SerializeField] private GameObject player;
    Vector3 puntoCentroPantalla = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
    Vector3 playerPosition;

    void Start()
    {
        dataText.text = nota.data; // asigna el texto de la nota al campo de texto
        fechaText.text = nota.fecha; // asigna la fecha de la nota al campo de texto
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Verificar si el botón derecho del ratón fue presionado
        {
            playerPosition = player.transform.position;
            Ray rayo = cam.ScreenPointToRay(puntoCentroPantalla);
            RaycastHit hit;
            float distancia = Vector3.Distance(transform.position, playerPosition);
            Debug.Log(distancia);
            if (distancia <= distanciaInteraccion)
            {
                if (Physics.Raycast(rayo, out hit))
                {
                    if (hit.collider.CompareTag("Nota"))
                    {
                        GameManager.instance.Interaction();
                        posIt.SetActive(true);
                    }
                }
            }
        }
    }
    public void EndInteraction()
    {
        GameManager.instance.currentState = GameState.Playing;
        posIt.SetActive(false);
    }
}
