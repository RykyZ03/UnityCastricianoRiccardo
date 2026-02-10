using UnityEngine;

public class DisappearingPlatform : MonoBehaviour
{
    public float visibleTime = 2f;
    public float hiddenTime = 2f;

    private Renderer[] renderers;
    private Collider[] colliders;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();

        StartCoroutine(PlatformCycle());
    }

    System.Collections.IEnumerator PlatformCycle()
    {
        while (true)
        {
            // Mostra tutto
            foreach (Renderer r in renderers)
                r.enabled = true;

            foreach (Collider c in colliders)
                c.enabled = true;

            yield return new WaitForSeconds(visibleTime);

            // Nasconde tutto
            foreach (Renderer r in renderers)
                r.enabled = false;

            foreach (Collider c in colliders)
                c.enabled = false;

            yield return new WaitForSeconds(hiddenTime);
        }
    }
}
