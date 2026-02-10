using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target to follow")]
    public Transform target;        // Il robot da seguire

    [Header("Offset")]
    public Vector3 offset = new Vector3(0f, 10f, -10f);  // Angolo isometrico tipico

    [Header("Smoothness")]
    public float smoothSpeed = 5f;  // Velocità di inseguimento

    void LateUpdate()
    {
        if (target == null)
            return;

        // Posizione target + offset
        Vector3 desiredPosition = target.position + offset;

        // Smooth follow
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Aggiorna posizione camera
        transform.position = smoothedPosition;

        // Guarda sempre il robot
        transform.LookAt(target);
    }
}
