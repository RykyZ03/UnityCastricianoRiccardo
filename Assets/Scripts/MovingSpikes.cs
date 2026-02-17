using UnityEngine;

public class MovingSpikes : MonoBehaviour
{
    [Header("Movimento Su e Giù")]
    public Transform pointA; // Punto più in basso
    public Transform pointB; // Punto più alto
    public float moveSpeed = 2f;

    [Header("Reset Player")]
    public Transform player;

    // Tiene traccia del punto di destinazione corrente

    private Transform currentTarget;

    void Start()
    {
        currentTarget = pointB;
    }

    void Update()
    {
        // Movimento su e giù tra i due punti

        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentTarget = (currentTarget == pointA) ? pointB : pointA;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerDeath>().Die();
        }
    }
}



