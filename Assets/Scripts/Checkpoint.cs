using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger colpito da: " + other.name);

        if (other.CompareTag("Player"))
        {
            PlayerDeath pd = other.GetComponent<PlayerDeath>();
            if (pd != null)
            {
                pd.SetCheckpoint(transform);
                Debug.Log("Checkpoint impostato!"); 
            }
            else
            {
                Debug.Log("PlayerDeath non trovato!"); 
            }
        }
        else
        {
            Debug.Log("Tag non è Player, è: " + other.tag); 
        }
    }
}