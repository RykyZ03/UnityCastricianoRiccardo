using UnityEngine;

public class RotatingHazard : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 180, 0);
    public Transform player;

    private void Update()
    {
        // Ruota in base alla velocità inserita
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerDeath>().Die();
        }
    }
}