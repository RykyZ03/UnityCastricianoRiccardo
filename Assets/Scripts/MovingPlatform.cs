using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movimento piattaforma")]
    public Vector3 moveDirection = Vector3.right;  // Direzione del movimento
    public float moveDistance = 5f;               // Quanto si muove avanti e indietro
    public float moveSpeed = 2f;                  // Velocità della piattaforma

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Salva posizione iniziale
    }

    void Update()
    {
        // Calcola oscillazione avanti/indietro con Sine
        transform.position = startPosition + moveDirection * Mathf.Sin(Time.time * moveSpeed) * moveDistance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
