using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip rightStep;
    public AudioClip leftStep;
    public float stepRate = 0.4f;

    private float stepTimer = 0f;
    private bool rightFoot = true;

    void Update()
    {
        // Usa input da tastiera per rilevare movimento
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        bool isMoving = Mathf.Abs(inputX) > 0.1f || Mathf.Abs(inputZ) > 0.1f;

        if (isMoving)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepRate)
            {
                stepTimer = 0f;

                AudioClip clip = rightFoot ? rightStep : leftStep;
                audioSource.PlayOneShot(clip);

                rightFoot = !rightFoot;
            }
        }
        else
        {
            stepTimer = stepRate; // reset timer se fermo
        }
    }
}
