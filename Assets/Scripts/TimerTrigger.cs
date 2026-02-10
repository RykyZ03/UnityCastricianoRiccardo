using UnityEngine;
using TMPro;

public class TimerTrigger : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timeElapsed = 0f;
    private bool timerActive = false;

    void Update()
    {
        if (timerActive)
        {
            timeElapsed += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timeElapsed / 60);
            int seconds = Mathf.FloorToInt(timeElapsed % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void OnTriggerEnter(Collider other)  // NOTA: OnTriggerEnter, non OnTriggerEnter2D
    {
        if (other.CompareTag("Player"))
        {
            timerActive = true;
            Debug.Log("Timer partito!");
        }
    }
}