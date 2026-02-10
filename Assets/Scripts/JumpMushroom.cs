using UnityEngine;

public class JumpMushroom : MonoBehaviour
{
    public float jumpForce = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MovementInput player = other.GetComponent<MovementInput>();

            if (player != null)
            {
                player.verticalVel = jumpForce;
            }
        }
    }
}
