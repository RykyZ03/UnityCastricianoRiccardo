using UnityEngine;

// Componente centrale per la morte del player.
// Supporta due modalità: permadeath (spawn fisso) e checkpoint (spawn dinamico).
public class PlayerDeath : MonoBehaviour
{
    [Header("Spawn")]
    // Punto di partenza fisso, usato sempre in permadeath e come fallback in checkpoint
    public Transform spawnPoint;

    [Header("Modalità")]
    // true = riparte sempre dallo spawn iniziale
    // false = riparte dall'ultimo checkpoint toccato
    public bool permadeath = true;

    // Ultimo checkpoint toccato, aggiornato da Checkpoint.cs
    private Transform currentCheckpoint;

    private CharacterController cc;
    private MovementInput mi;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        mi = GetComponent<MovementInput>();

        // All'inizio il checkpoint corrente coincide con lo spawn point
        currentCheckpoint = spawnPoint;
    }

    // Chiamato da Checkpoint.cs quando il player tocca un checkpoint
    public void SetCheckpoint(Transform newCheckpoint)
    {
        // Aggiorna solo se non è già il checkpoint attivo
        if (!permadeath && currentCheckpoint != newCheckpoint)
        {
            currentCheckpoint = newCheckpoint;
            Debug.Log("Checkpoint aggiornato: " + newCheckpoint.name);
        }
    }

    public void Die()
    {
        // In permadeath riparte sempre dallo spawn, altrimenti dall'ultimo checkpoint
        Transform respawnTarget = permadeath ? spawnPoint : currentCheckpoint;

        // Disabilita il CharacterController prima di spostare il player
        cc.enabled = false;
        transform.position = respawnTarget.position;
        cc.enabled = true;

        // Azzera la velocità verticale per evitare che riparta in caduta
        mi.ResetVerticalVelocity();
    }
}