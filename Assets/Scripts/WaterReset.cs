using UnityEngine;

// Trigger acqua: riporta il player allo spawn se ci cade dentro.
public class WaterReset : MonoBehaviour
{
    public Transform player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerDeath>().Die();
        }
    }
}