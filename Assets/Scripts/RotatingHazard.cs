using UnityEngine;

public class RotatingHazard : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 180, 0);

    public Transform player;
    public Transform startPoint;

    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController cc = player.GetComponent<CharacterController>();

            cc.enabled = false;
            player.position = startPoint.position;
            cc.enabled = true;

            MovementInput mi = player.GetComponent<MovementInput>();
            if (mi != null)
                mi.ResetVerticalVelocity();
        }
    }
}
