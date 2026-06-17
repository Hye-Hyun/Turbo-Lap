using UnityEngine;

public class FinishLine: MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
        {
            return;
        }

        if (RaceManager.Instance.passedCheckpoint)
        {
            RaceManager.Instance.currentLap++;

            Debug.Log($"Lap {RaceManager.Instance.currentLap} completed!");

            RaceManager.Instance.passedCheckpoint = false;

            if (RaceManager.Instance.currentLap >= RaceManager.Instance.targetLap)
            {
                Debug.Log("Race Finished!");
                Time.timeScale = 0f;
            }
        }
    }
}
