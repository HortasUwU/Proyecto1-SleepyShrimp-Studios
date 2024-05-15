using TMPro;
using UnityEngine;
using System.Collections;
using static GameManager;

public class PlayerManager : MonoBehaviour
{
    // Singleton instance
    public static PlayerManager instance;

    // Player variables
    public int health = 5;
    private Rigidbody rb;
    public TextMeshProUGUI healthText;
    public float damageInterval = 2f; // Intervalo de tiempo entre tomas de daño en segundos
    private float lastDamageTime;

    // Variables para el efecto de sacudida de pantalla
    public float shakeIntensity = 0.1f;
    public float shakeDuration = 0.5f;
    [SerializeField] GameObject playerSpawner;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        UpdateHealthText();

}

void Start()
    {
        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();
        spawnear();
        lastDamageTime = -damageInterval;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si ha pasado suficiente tiempo desde el último daño
        if (Time.time - lastDamageTime >= damageInterval)
        {
            // Check if the collided object has the "Enemy" tag
            if (collision.gameObject.CompareTag("Enemy"))
            {
                // Reduce player health
                health--;

                // Update health text after taking damage
                UpdateHealthText();

                // Marcar el tiempo del último daño
                lastDamageTime = Time.time;

                // Shake screen
                StartCoroutine(ShakeScreen());

                // Check if player health has reached zero
                if (health <= 0)
                {
                    GameManager.instance.EndGame();
                }
            }
        }
    }

    // Función para sacudir la pantalla
    private IEnumerator ShakeScreen()
    {
        Vector3 originalPosition = Camera.main.transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeIntensity;
            float y = Random.Range(-1f, 1f) * shakeIntensity;

            Camera.main.transform.localPosition = originalPosition + new Vector3(x, y, 0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.localPosition = originalPosition;
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Vida: " + health.ToString();
        }
        else
        {
            Debug.LogError("El objeto de texto para mostrar la vida no está asignado en el inspector.");
        }
    }

    public void spawnear()
    {
        // Restablece la salud del jugador a 3
        health = 3;

        // Actualiza el texto de la salud
        UpdateHealthText();

        // Busca el objeto de aparición del jugador por su etiqueta
        GameObject playerSpawner = GameObject.FindWithTag("PlayerSpawner");

        // Verificar si se encontró el objeto de aparición del jugador
        if (playerSpawner != null)
        {
            Debug.LogError(playerSpawner.transform.position);
            gameObject.transform.position = playerSpawner.transform.position;
            gameObject.transform.rotation = Quaternion.identity; 
            Debug.LogError("funciona");

        }
        else
        {
            Debug.LogError("No se encontró un objeto con la etiqueta 'PlayerSpawner'.");
        }
    }

}