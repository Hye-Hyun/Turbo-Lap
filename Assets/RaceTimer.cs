using UnityEngine;
using TMPro;

public class RaceTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private float elapsedTime;
    private bool isRunning = false;

    public float ElapsedTime => elapsedTime;
     
    // Update is called once per frame
    void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        float seconds = elapsedTime % 60;

        timerText.text = string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
    }
}
