using UnityEngine;

public class MovingSpikes : MonoBehaviour
{
    [Header("Movimento Su e Giù")]
    public Transform pointA; // Punto più in basso
    public Transform pointB; // Punto più in alto
    public float moveSpeed = 2f;

    [Header("Reset Player")]
    public Transform player;
    public Transform spawnPoint;

    private Transform currentTarget;

    void Start()
    {
        currentTarget = pointB;
    }

    void Update()
    {
        // Movimento su e giù
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);

        // Cambia direzione quando raggiunge il punto
        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentTarget = (currentTarget == pointA) ? pointB : pointA;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
                player.position = spawnPoint.position;
                cc.enabled = true;

                MovementInput mi = player.GetComponent<MovementInput>();
                if (mi != null)
                    mi.ResetVerticalVelocity();
            }
        }
    }
}