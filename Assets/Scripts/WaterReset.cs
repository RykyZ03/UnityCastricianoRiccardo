using UnityEngine;

public class WaterReset : MonoBehaviour
{
    public Transform player;       // Il robot
    public Transform startPoint;   // Punto di reset

    private CharacterController cc;

    void Start()
    {
        cc = player.GetComponent<CharacterController>();
        if (cc == null)
            Debug.LogError("Il player deve avere CharacterController!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Disabilita temporaneamente il CharacterController
            cc.enabled = false;

            // Riporta il robot al punto di partenza
            player.position = startPoint.position;

            // Riattiva il CharacterController
            cc.enabled = true;

            // Reset velocità verticale tramite funzione pubblica
            MovementInput mi = player.GetComponent<MovementInput>();
            if (mi != null)
            {
                mi.ResetVerticalVelocity();
            }
        }
    }
}
