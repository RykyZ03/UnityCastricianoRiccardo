using UnityEngine;

public class RotatingBlade : MonoBehaviour
{
    [Header("Rotazione")]
    public Vector3 rotationSpeed = new Vector3(0, 180, 0);

    [Header("Movimento Avanti/Indietro")]
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;

    [Header("Reset Player")]
    public Transform player;

    private Transform currentTarget;

    void Start()
    {
        currentTarget = pointB;
    }

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
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