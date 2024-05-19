using UnityEngine;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private int distanciaInteraccion = 10;
    [SerializeField] private LayerMask capasInteractuables; // Capas que pueden ser interactuadas
    public Camera cam;
    public GameObject player;
    public Image punteroImage; // Referencia al objeto de imagen del puntero
    public float maxSize = 2f; // Tamaño máximo del puntero cuando está en la distancia de interacción
    public Color colorInteractuable = Color.green; // Color del puntero cuando puede interactuar
    public Color colorNoInteractuable = Color.red; // Color del puntero cuando no puede interactuar

    private Vector3 puntoCentroPantalla;

    void Start()
    {
        puntoCentroPantalla = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (IsPlayerInRange())
            {
                Interact();
            }
        }

        UpdatePuntero();
    }

    private bool IsPlayerInRange()
    {
        if (cam == null || player == null) return false;

        Ray rayo = cam.ScreenPointToRay(puntoCentroPantalla);
        RaycastHit hit;

        if (Physics.Raycast(rayo, out hit, distanciaInteraccion, capasInteractuables))
        {
            return hit.collider.gameObject == gameObject;
        }

        return false;
    }

    protected abstract void Interact();

    private void UpdatePuntero()
    {
        RaycastHit hit;
        Ray rayo = cam.ScreenPointToRay(puntoCentroPantalla);

        if (Physics.Raycast(rayo, out hit, distanciaInteraccion, capasInteractuables))
        {
            float distanceToHit = Vector3.Distance(cam.transform.position, hit.point);
            float t = Mathf.Clamp01(distanceToHit / distanciaInteraccion);
            float newSize = Mathf.Lerp(1f, maxSize, t);

            punteroImage.rectTransform.localScale = Vector3.one * newSize;
            punteroImage.color = colorInteractuable;
        }
        else
        {
            punteroImage.rectTransform.localScale = Vector3.one;
            punteroImage.color = colorNoInteractuable;
        }
    }
}
