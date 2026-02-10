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
    public Transform spawnPoint;

    private Transform currentTarget;

    void Start()
    {
        currentTarget = pointB;
    }

    void Update()
    {
        // Rotazione lama
        transform.Rotate(rotationSpeed * Time.deltaTime);

        // Movimento avanti e indietro
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